using System;
using System.IO;
using System.Text.Json;

namespace CommunityToolkit.Wpf.Template.Models;
/****************************************************************************
   Purpose      : 
       애플리케이션의 환경 설정 정보를 저장하고 불러오는 역할을 담당하는 모델 클래스입니다.
       JSON 형식의 설정 파일(`config.json`)을 로컬에서 읽고 쓰는 기능을 제공합니다.
       설정 항목에는 애플리케이션 이름, 버전, 구성 파일 경로 등이 포함됩니다.

   Created By   : $username$                                                 
   Created On   : $time$                                                   
   Department   : $department$                                                   
   Company      : $company$                                       
   Email        : $email$                                        
   ****************************************************************************/
public class SetupModel
{
    // 애플리케이션 이름
    public string ApplicationName { get; set; } = "WPF 애플리케이션";

    // 버전 정보
    public string Version { get; set; } = "1.0.0";

    // 구성 파일 경로
    public string ConfigPath { get; set; } = "config.json";

    // 테마 구성
    public string Theme { get; set; } = "Light";


    // 생성자 - 객체 생성 시 설정 파일을 자동으로 로드합니다.
    public SetupModel()
    {
        LoadDefaultSettings();
    }

    /// <summary>
    /// config.json 파일에서 설정 정보를 로드합니다.
    /// 파일이 없거나 오류가 발생하면 기본값을 유지합니다.
    /// </summary>
    private void LoadDefaultSettings()
    {
        try
        {
            if (File.Exists(ConfigPath))
            {
                var json = File.ReadAllText(ConfigPath);
                var settings = JsonSerializer.Deserialize<SetupModel>(json);

                if (settings != null)
                {
                    ApplicationName = settings.ApplicationName;
                    Version = settings.Version;
                    // 필요한 경우 다른 속성들도 복사
                }
            }
        }
        catch (Exception)
        {
            // 오류 발생 시 기본 설정 유지
        }
    }

    /// <summary>
    /// 현재 설정 내용을 config.json 파일에 저장합니다.
    /// </summary>
    public async Task SaveSettingsAsync()
    {
        try
        {
            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true // 보기 좋게 들여쓰기 적용
            });

            await File.WriteAllTextAsync(ConfigPath, json);
        }
        catch (Exception)
        {
            // 저장 실패 시 처리 로직 (필요 시 로그 추가)
        }
    }
}