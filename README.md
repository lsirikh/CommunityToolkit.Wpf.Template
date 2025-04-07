# CommunityToolkit.Wpf.Template

### Current Version : v1.0.0

---

### ✅ Goal  
> `CommunityToolkit.Wpf.Template`는 **.NET 기반의 WPF MVVM 애플리케이션을 빠르게 시작하기 위한 템플릿 프로젝트**입니다.  
CommunityToolkit.MVVM과 Autofac을 기반으로 한 구조화된 MVVM 패턴을 제공합니다.

---

## 1. 템플릿 개요

### 1.1 개요  
이 템플릿은 다음과 같은 기능을 기본 제공하며, **MVVM 설계에 익숙하지 않은 개발자도 쉽게 구조를 이해하고 확장**할 수 있도록 구성되어 있습니다.

- `CommunityToolkit.Mvvm` 기반 MVVM 구조
- `Autofac` 기반 DI(Dependency Injection)
- 메시징 중심 ViewModel 전환 구조 (Messenger)
- `BootstrapperBase` 기반 앱 수명 주기 관리
- `ViewLocatorTemplateSelector` 기반 ViewModel ↔ View 바인딩 자동화

---

### 1.2 프로젝트 기본 구조

#### **📂 ViewModels**
> MVVM 구조의 ViewModel 폴더

- `ShellViewModel.cs`  
  - 애플리케이션의 메인 뷰모델
- `InfoViewModel.cs`  
  - 정보 화면 ViewModel
- `DataViewModel.cs`  
  - 데이터 처리 및 명령 실행 ViewModel

#### **📂 Views**
> XAML UI 화면

- `ShellView.xaml`  
  - 메인 Layout 화면
- `InfoView.xaml`  
  - 정보 출력 화면
- `DataView.xaml`  
  - 데이터 처리 화면

#### **📂 Models**
> 데이터 구조 정의

- `SetupModel.cs`  
  - 설정 정보 관리 모델
- `InfoModel.cs`  
  - 정보 출력용 데이터 모델

#### **📂 Messages**
> MVVM 간 메시지 통신 구조

- `NavigationMessage.cs`  
  - ViewModel 전환 메시지
- `StatusChangedMessage.cs`  
  - 상태 메시지 전파
- `PanelClosedMessage.cs`, `CancellationRequestedMessage.cs`  
  - 패널 종료, 취소 요청 메시지

#### **📂 Navigations**
> 패널 전환 관리자

- `PanelNavigationManager.cs`  
  - 메시지를 통해 ViewModel 전환을 수행

#### **📂 Templates**
> ViewModel ↔ View 자동 매핑 템플릿

- `ViewLocatorTemplateSelector.cs`  
  - `MainViewModel` → `MainView` 네이밍 기반으로 View 생성

#### **📂 Bootstrapping**
> 앱 시작, DI 구성, ViewModel 실행

- `App.xaml.cs`  
  - 애플리케이션 시작 진입점
- `Bootstrapper.cs`  
  - DI 및 중복 실행 방지, ViewModel 실행
- `BootstrapperBase.cs`  
  - DI + 수명주기 처리 공통 템플릿

---

## 2. 주요 기술 스택

| 항목 | 내용 |
|------|------|
| 플랫폼 | .NET 7.0 / .NET 8.0 (Windows Desktop) |
| UI 프레임워크 | WPF |
| MVVM 프레임워크 | [CommunityToolkit.Mvvm](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/) |
| 의존성 주입 | Autofac |
| 메시징 시스템 | CommunityToolkit.Mvvm.Messaging |
| 템플릿 엔진 | dotnet CLI 템플릿 (`dotnet new`) |

---

## 3. 설치 및 사용

### 3.1 설치

```bash
dotnet new install CommunityToolkit.Wpf.Template
