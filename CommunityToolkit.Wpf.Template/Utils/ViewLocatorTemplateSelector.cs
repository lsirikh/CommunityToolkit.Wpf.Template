using System;
using System.Windows.Controls;
using System.Windows;

namespace CommunityToolkit.Wpf.Template.Utils;
/****************************************************************************
   Purpose      : 
       ViewModel 타입 이름에 따라 자동으로 대응되는 View를 찾아 바인딩해주는 
       WPF DataTemplateSelector 클래스입니다. ViewModel ↔ View 매핑을 
       일관된 네이밍 규칙(ViewModel → View)으로 처리하여 템플릿 로딩을 자동화합니다.

       예: MainViewModel → MainView
       
   Created By   : $username$                                                 
   Created On   : $time$                                                   
   Department   : $department$                                                    
   Company      : $company$                                       
   Email        : $email$                                        
****************************************************************************/

public class ViewLocatorTemplateSelector : DataTemplateSelector
{
    // DataTemplate을 선택하는 메서드: ViewModel에 대응하는 View를 찾아 동적으로 생성합니다.
    public override DataTemplate? SelectTemplate(object item, DependencyObject container)
    {
        // ViewModel이 null이면 처리하지 않음
        if (item == null) return null;

        // ViewModel 타입을 가져옴
        var viewModelType = item.GetType();

        // ViewModel 명칭에서 "ViewModel"을 "View"로 치환하여 대응되는 View 클래스명을 구성
        var viewTypeName = viewModelType.FullName?.Replace("ViewModel", "View");

        // ViewModel이 속한 어셈블리에서 View를 찾기 위해 어셈블리 정보 획득
        var viewAssembly = viewModelType.Assembly;

        if (viewTypeName == null) return null;

        // 어셈블리에서 해당 View 타입을 가져옴
        var viewType = viewAssembly.GetType(viewTypeName);
        if (viewType == null) return null;

        // View 인스턴스를 생성 (생성 실패 시 null 반환)
        var view = Activator.CreateInstance(viewType) as FrameworkElement;
        if (view == null) return null;

        // 생성한 View에 ViewModel을 바인딩
        view.DataContext = item;

        // View를 VisualTree에 삽입한 DataTemplate을 반환
        return new DataTemplate
        {
            VisualTree = new FrameworkElementFactory(viewType)
        };
    }
}
