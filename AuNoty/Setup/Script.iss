; ///////////////////////////////////////////////////////////
; // Graphical Installer for Inno Setup                    //
; // Version 3.9.01 (Radka)                                //
; // Copyright (c) 2011 - 2018 unSigned, s. r. o.          //
; // http://www.graphical-installer.com                    //
; // customers@unsigned.sk                                 //
; // All Rights Reserved.                                  //
; ///////////////////////////////////////////////////////////
 
; *********************************************
; *              Main script file.            *
; ********************************************* 
 
; Script generated with Graphical Installer Wizard.
 
; This identifier is used for compiling script as Graphical Installer powered installer. Comment it out for regular compiling.
#define GRAPHICAL_INSTALLER_PROJECT
 
#ifdef GRAPHICAL_INSTALLER_PROJECT
    ; File with setting for graphical interface
    #include "Script.graphics.iss"
#else
    ; Default UI file
    #define public GraphicalInstallerUI ""
#endif
 
; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "AuNoty"
#define MyAppVersion "1.0"
#define MyAppPublisher "Aumatic"
#define MyAppURL "http://aumatic.com/"
#define MyAppExeName "MyProg.exe"

[Setup]
PrivilegesRequired = admin     
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
;AppId={{9AB7D14E-42B2-4C5D-B36E-1623D8733888}
;AppName={#MyAppName}
;AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
;AppPublisher={#MyAppPublisher}
;AppPublisherURL={#MyAppURL}
;AppSupportURL={#MyAppURL}
;AppUpdatesURL={#MyAppURL}
;DefaultDirName={pf}\Aumatic\AuNoty
;DefaultGroupName=Aumatic\AuNoty
;DisableProgramGroupPage=yes
;OutputBaseFilename=setup
;Compression=lzma
;SolidCompression=yes
; Directive "WizardSmallImageBackColor" was modified for purposes of Graphical Installer.
;WizardSmallImageBackColor={#GraphicalInstallerUI}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "polish"; MessagesFile: "compiler:Languages\Polish.isl"
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Files]
Source: "..\AuNoty\bin\Release\error.png"; DestDir: "{app}"
Source: "..\AuNoty\bin\Release\info.png"; DestDir: "{app}"
Source: "..\AuNoty\bin\Release\warning.png"; DestDir: "{app}"
Source: "..\AuNoty\bin\Release\question.png"; DestDir: "{app}"
Source: "..\AuNoty\bin\Release\txt.txt"; DestDir: "{app}"
Source: "..\AuNoty\bin\Release\AuNoty.exe"; DestDir: "{app}"
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\AuNoty"; Filename: "{app}\AuNoty.exe"

[Tasks]
Name: "TaskEntry"; Description: "Install for all users on this computer"; GroupDescription: "Please select whether you wish to make AuNoty available to all users, or just yourself";  Flags: exclusive;

Name: "TaskEntry2"; Description: "Install for current user"; GroupDescription: "Please select whether you wish to make AuNoty available to all users, or just yourself"; Flags: exclusive unchecked;



[Registry]
   Root: HKLM; Subkey: "SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Run"; Permissions: users-modify; \
   Flags: uninsdeletekey createvalueifdoesntexist; Tasks: TaskEntry; ValueType: string; \
   ValueName: "AuNoty"; ValueData: """{app}\AuNoty.exe"""

   Root: HKCU; Subkey: "SSOFTWARE\Microsoft\Windows\CurrentVersion\Run"; Permissions: users-modify; \
   Flags: uninsdeletekey createvalueifdoesntexist; Tasks: TaskEntry2; ValueType: string; \
   ValueName: "AuNoty"; ValueData: """{app}\AuNoty.exe"""


[Code]
// Next functions are used for proper working of Graphical Installer powered installer
procedure InitializeWizard();
begin
    #ifdef GRAPHICAL_INSTALLER_PROJECT
    InitGraphicalInstaller();
    #endif
end;
 
procedure CurStepChanged(CurStep: TSetupStep);
begin

 
  //MsgBox(ExpandConstant('{language}'), mbInformation, MB_OK); 

  if ExpandConstant('{language}') = 'polish' then
  begin
	  if CurStep = ssPostInstall then
	  begin;
		if MsgBox('Uruchamiaj przy starcie systemu Windows', mbConfirmation, MB_YESNO) = IDYES then
		begin
		   RegWriteStringValue
		   (HKCU, 'SOFTWARE\Wow6432Node\Windows\CurrentVersion\Run','AuNoty', ExpandConstant('{app}\AuNoty.exe')); 
		end;
	  end;
  end;



  if ExpandConstant('{language}') = 'english' then
  begin
	  if CurStep = ssPostInstall then
	  begin;
		if MsgBox('Run on startup windows?', mbConfirmation, MB_YESNO) = IDYES then
		begin
		   RegWriteStringValue
		   (HKEY_CURRENT_USER, 'SOFTWARE\Microsoft\Windows\CurrentVersion\Run','AuNoty', ExpandConstant('{app}\AuNoty.exe')); 
		end;
	  end;
  end;


    if ExpandConstant('{language}') = 'spanish' then
  begin
	  if CurStep = ssPostInstall then
	  begin;
		if MsgBox('Iniciar al inicio de Windows?', mbConfirmation, MB_YESNO) = IDYES then
		begin
		   RegWriteStringValue
		   (HKEY_CURRENT_USER, 'SOFTWARE\Microsoft\Windows\CurrentVersion\Run','AuNoty', ExpandConstant('{app}\AuNoty.exe')); 
		end;
	  end;
  end;


end;

procedure CurPageChanged(CurPageID: Integer);
begin
    #ifdef GRAPHICAL_INSTALLER_PROJECT
    PageChangedGraphicalInstaller(CurPageID);
    #endif
end;
 
procedure DeInitializeSetup();
begin
    #ifdef GRAPHICAL_INSTALLER_PROJECT
    DeInitGraphicalInstaller();
    #endif
end;
 
// End of file (EOF)