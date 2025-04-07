using Autofac;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;

namespace CommunityToolkit.Wpf.Template.Base;
/****************************************************************************
   Purpose      : 
       WPF 애플리케이션의 부트스트래퍼 템플릿 클래스입니다.
       DI 컨테이너 구성, 초기화, 뷰 생성 및 시작, 종료 처리까지
       애플리케이션의 전체 생명주기를 관리합니다.

       이 클래스는 제네릭으로 View와 ViewModel을 전달받아
       자동으로 바인딩 및 실행하며, CommunityToolkit의 Messenger 및
       Autofac DI 컨테이너를 사용하는 구조로 구성되어 있습니다.

       하위 클래스에서는 Configure(), OnStartedAsync(), OnStoppedAsync()
       메서드를 구현하여 의존성 등록과 시작/종료 처리를 정의합니다.

   Created By   : $username$                                                 
   Created On   : $time$                                                   
   Department   : $department$                                                    
   Company      : $company$                                       
   Email        : $email$                                          
****************************************************************************/
public abstract class BootstrapperBase<TView, TViewModel>
    where TView : Window
    where TViewModel : class
{
    // 내부 Autofac 컨테이너 인스턴스
    private static IContainer? _container;

    // 외부 접근 가능한 정적 컨테이너 프로퍼티 (App 등에서 접근 가능)
    public static IContainer? Container => _container;

    // 취소 토큰 소스 (비동기 작업 취소 처리용)
    protected CancellationTokenSource CancellationTokenSourceHandler { get; } = new();

    /// <summary>
    /// DI 컨테이너 구성 메서드 (비동기 초기화용)
    /// </summary>
    public async Task InitialAsync()
    {
        ConfigureContainer();
        await Task.CompletedTask;
    }

    /// <summary>
    /// 애플리케이션 시작 처리 (뷰 생성, 바인딩, 출력)
    /// </summary>
    public async Task StartAsync()
    {
        // 시작 직후의 추가 초기화 처리 (예: 설정 로딩, 서비스 호출 등)
        await OnStartedAsync(CancellationTokenSourceHandler.Token);

        // DI로 View 및 ViewModel 인스턴스 Resolve
        var view = _container?.Resolve<TView>();
        var viewModel = _container?.Resolve<TViewModel>();

        // ViewModel을 View에 바인딩 후 View 실행
        if (view != null && viewModel != null)
        {
            view.DataContext = viewModel;
            view.Show();
        }
    }

    /// <summary>
    /// 애플리케이션 종료 처리 (리소스 해제 등)
    /// </summary>
    public async Task StopAsync()
    {
        await OnStoppedAsync();

        CancellationTokenSourceHandler.Cancel();
        CancellationTokenSourceHandler.Dispose();

        _container?.Dispose();
    }

    /// <summary>
    /// Autofac 컨테이너 구성 로직 정의
    /// 기본 타입, Messenger, View, ViewModel 등을 등록
    /// </summary>
    private void ConfigureContainer()
    {
        var builder = new ContainerBuilder();

        // Toolkit Messenger 기본 등록
        RegisterFrameworkTypes(builder);

        // ViewModel 및 View는 싱글 인스턴스로 등록
        builder.RegisterType<TViewModel>().AsSelf().SingleInstance();
        builder.RegisterType<TView>().AsSelf().SingleInstance();

        // Messenger 인스턴스 수동 등록
        var messenger = WeakReferenceMessenger.Default;
        builder.RegisterInstance(messenger).As<IMessenger>().SingleInstance();

        // 하위 클래스에서 구현한 사용자 정의 서비스/설정 등록
        Configure(builder);

        // DI 컨테이너 빌드
        _container = builder.Build();
    }

    /// <summary>
    /// 프레임워크 핵심 타입 등록 (중복 방지용도)
    /// </summary>
    private void RegisterFrameworkTypes(ContainerBuilder builder)
    {
        builder.RegisterInstance(WeakReferenceMessenger.Default).As<IMessenger>().SingleInstance();
    }

    /// <summary>
    /// 하위 클래스에서 구현: 시작 시 로직 정의 (비동기)
    /// </summary>
    protected abstract Task OnStartedAsync(CancellationToken token);

    /// <summary>
    /// 하위 클래스에서 구현: 종료 시 로직 정의
    /// </summary>
    protected abstract Task OnStoppedAsync();

    /// <summary>
    /// 하위 클래스에서 구현: 사용자 서비스, 모델 등의 DI 등록
    /// </summary>
    protected abstract void Configure(ContainerBuilder builder);
}