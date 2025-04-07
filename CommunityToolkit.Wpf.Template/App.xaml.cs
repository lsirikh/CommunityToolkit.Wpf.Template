using Autofac;
using System.Configuration;
using System.Data;
using System.Windows;

namespace CommunityToolkit.Wpf.Template;
/****************************************************************************
   Purpose      : 
       WPF 애플리케이션의 진입점으로서, 전체 애플리케이션의 수명주기 관리 및 
       의존성 주입 컨테이너(AutoFac)의 초기화와 실행 환경 설정을 담당합니다.
       Bootstrapper를 통해 ViewModel, 서비스 등록 및 초기 화면 구성을 수행합니다.
                                                                           
   Created By   : $username$                                                 
   Created On   : $time$                                                   
   Department   : $department$                                                   
   Company      : $company$                                       
   Email        : $email$                                        
****************************************************************************/

// WPF 애플리케이션의 진입점을 정의하는 App 클래스입니다.
public partial class App : Application
{
    // Bootstrapper는 DI 컨테이너 초기화 및 애플리케이션 시작/종료 로직을 담당합니다.
    private Bootstrapper? _bootstrapper;

    // 정적 Container 속성을 통해 앱 어디서든 Autofac DI 컨테이너에 접근할 수 있도록 합니다.
    public static IContainer? Container { get; private set; }

    // 애플리케이션이 시작될 때 호출되는 메서드입니다.
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Bootstrapper 인스턴스를 생성합니다.
        _bootstrapper = new Bootstrapper();

        // 비동기 초기화: 서비스 등록, ViewModel 구성 등 DI 관련 작업을 수행합니다.
        await _bootstrapper.InitialAsync();

        // 초기화된 Bootstrapper의 DI 컨테이너를 전역(static)으로 복사하여 다른 클래스에서도 사용 가능하게 합니다.
        Container = Bootstrapper.Container;

        // 애플리케이션 실행 시작: 첫 View를 띄우거나 필요한 UI 로직을 처리합니다.
        await _bootstrapper.StartAsync();
    }

    // 애플리케이션이 종료될 때 호출되는 메서드입니다.
    protected override async void OnExit(ExitEventArgs e)
    {
        // Bootstrapper가 초기화되었을 경우에만 종료 작업 수행
        if (_bootstrapper != null)
        {
            // DI 컨테이너 정리, 리소스 해제 등 필요한 종료 작업을 수행합니다.
            await _bootstrapper.StopAsync();
        }

        base.OnExit(e);
    }
}


