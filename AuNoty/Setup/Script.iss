; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "AuNoty"
#define MyAppVersion "1.0"
#define MyAppPublisher "Aumatic"
#define MyAppURL "http://aumatic.com/"
#define MyAppExeName "MyProg.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{9AB7D14E-42B2-4C5D-B36E-1623D8733888}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\Aumatic\AuNoty
DefaultGroupName=Aumatic\AuNoty
DisableProgramGroupPage=yes
OutputBaseFilename=setup
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "..\AuNoty\bin\Release\AuNoty.exe"; DestDir: "{app}"
Source: "..\AuNoty\bin\Release\error.png"; DestDir: "{app}"
Source: "..\AuNoty\bin\Release\info.png"; DestDir: "{app}"
Source: "..\AuNoty\bin\Release\warning.png"; DestDir: "{app}"
Source: "..\AuNoty\bin\Release\question.png"; DestDir: "{app}"
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\AuNoty"; Filename: "{app}\AuNoty.exe"


[Registry]
Root: HKCU; Subkey: "SOFTWARE\Microsoft\Windows\CurrentVersion\Run"; ValueType: string; ValueName: "AuNoty"; ValueData: """{app}\AuNoty.exe"""; Flags: uninsdeletevalue


