# CommunityToolkit WPF Template

.NET 8 / WPF 애플리케이션의 MVVM 시작 구조를 구성한 템플릿입니다. CommunityToolkit.Mvvm의 속성·명령 생성과 메시징을 사용하고 Autofac으로 의존성을 연결합니다.

## 포함된 예제

- Bootstrapper와 ViewModel 기반 클래스
- `ObservableProperty`, 취소 가능한 `RelayCommand`, 실행 조건 처리
- `IMessenger`를 통한 화면 간 상태 전달
- 데이터 패널과 정보 패널 분리

[DataViewModel](CommunityToolkit.Wpf.Template/ViewModels/Panels/DataViewModel.cs)의 데이터 로딩은 `Task.Delay`를 이용한 동작 예제입니다. 실제 데이터 서비스는 애플리케이션에 맞게 연결해야 합니다.

## 개발 환경

Windows, .NET 8 SDK, WPF 개발 도구가 필요합니다. 프로젝트의 주요 패키지는 CommunityToolkit.Mvvm 8.4.0과 Autofac 8.2.1입니다.

[프로젝트 폴더](CommunityToolkit.Wpf.Template)를 열어 패키지를 복원하고 빌드합니다. 템플릿 메타데이터에는 Sensorway 명칭과 파일 치환용 항목이 있으므로, 새 프로젝트에 적용할 때 작성자·회사·리소스·설정을 맞춰야 합니다.
