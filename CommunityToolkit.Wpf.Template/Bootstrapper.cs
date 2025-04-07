using Autofac;
using CommunityToolkit.Wpf.Template.Base;
using CommunityToolkit.Wpf.Template.Models;
using CommunityToolkit.Wpf.Template.ViewModels;
using CommunityToolkit.Wpf.Template.ViewModels.Navigations;
using CommunityToolkit.Wpf.Template.ViewModels.Panels;
using CommunityToolkit.Wpf.Template.Views;
using System;
using System.Diagnostics;
using System.Windows;

namespace CommunityToolkit.Wpf.Template;
/****************************************************************************
   Purpose      : 
       애플리케이션의 실행 진입점에서 사용할 구체적인 부트스트래퍼 클래스입니다.
       BootstrapperBase<ShellView, ShellViewModel>를 상속하여, 
       앱 시작 시 중복 실행 방지, DI 설정, 리소스 정리 등을 담당합니다.

       - Configure() : 필요한 서비스 및 ViewModel을 DI 컨테이너에 등록
       - OnStartedAsync() : 프로세스 중복 실행 여부 확인
       - OnStoppedAsync() : 종료 시 정리 로직 처리

   Created By   : $username$                                                 
   Created On   : $time$                                                   
   Department   : $department$                                                   
   Company      : $company$                                       
   Email        : $email$                                        
****************************************************************************/
public class Bootstrapper : BootstrapperBase<ShellView, ShellViewModel>
{
#if DEBUG
    /// <summary>
    /// 디버그 빌드에서 실행될 초기화 로직
    /// 중복 실행을 방지하기 위한 프로세스 검사 수행
    /// </summary>
    protected override Task OnStartedAsync(CancellationToken token)
    {
        try
        {
            // 현재 실행 중인 프로세스 정보 가져오기
            using var current = Process.GetCurrentProcess();
            var processName = current.ProcessName;

            // 동일 이름의 프로세스가 여러 개 있는지 확인
            var procs = Process.GetProcessesByName(processName);

            if (procs.Length > 1)
            {
                // 어셈블리 타이틀 가져오기 (UI에 표시할 이름)
                var entryAssembly = System.Reflection.Assembly.GetEntryAssembly();
                var titleAttribute = entryAssembly?
                    .GetCustomAttributes(typeof(System.Reflection.AssemblyTitleAttribute), true)
                    .OfType<System.Reflection.AssemblyTitleAttribute>()
                    .FirstOrDefault();

                var appTitle = titleAttribute?.Title ?? processName;

                // 사용자에게 알림 표시 후 종료
                MessageBox.Show($"{appTitle} is already running...", "Redundant Execution",
                                MessageBoxButton.OK, MessageBoxImage.Warning);

                Application.Current.Shutdown();
                return Task.CompletedTask;
            }
        }
        catch (Exception)
        {
            // 예외 무시 (필요 시 로깅 가능)
        }

        return Task.CompletedTask;
    }
#else
    /// <summary>
    /// 릴리즈 빌드에서 실행될 초기화 로직
    /// 중복 실행 방지 로직은 동일하며 로깅/예외처리 구분 없음
    /// </summary>
    protected override Task OnStartedAsync(CancellationToken token)
    {
        try
        {
            using var current = Process.GetCurrentProcess();
            var processName = current.ProcessName;

            var procs = Process.GetProcessesByName(processName);

            if (procs.Length > 1)
            {
                var entryAssembly = System.Reflection.Assembly.GetEntryAssembly();
                var titleAttribute = entryAssembly?
                    .GetCustomAttributes(typeof(System.Reflection.AssemblyTitleAttribute), true)
                    .OfType<System.Reflection.AssemblyTitleAttribute>()
                    .FirstOrDefault();

                var appTitle = titleAttribute?.Title ?? processName;

                MessageBox.Show($"{appTitle} is already running...", "Redundant Execution",
                                MessageBoxButton.OK, MessageBoxImage.Warning);

                Application.Current.Shutdown();
                return Task.CompletedTask;
            }
        }
        catch (Exception)
        {
        }

        return Task.CompletedTask;
    }
#endif

    /// <summary>
    /// 애플리케이션 종료 시 호출되는 로직 (현재는 정리 작업 없음)
    /// </summary>
    protected override Task OnStoppedAsync()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// DI 컨테이너 구성: ViewModel 및 서비스들을 등록
    /// </summary>
    protected override void Configure(ContainerBuilder builder)
    {
        var setup = new SetupModel(); // 설정 파일 로딩 포함
        setup.LoadDefaultSettings();
        // DI 컨테이너에 싱글 인스턴스로 등록
        builder.RegisterInstance(setup).AsSelf().SingleInstance();
        builder.RegisterType<PanelNavigationManager>().AsSelf().SingleInstance();
        builder.RegisterType<DataViewModel>().AsSelf().SingleInstance();
        builder.RegisterType<InfoViewModel>().AsSelf().SingleInstance();
    }
}
