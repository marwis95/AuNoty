; ///////////////////////////////////////////////////////////
; // Graphical Installer for Inno Setup                    //
; // Version 3.9.01 (Radka)                                //
; // Copyright (c) 2011 - 2018 unSigned, s. r. o.          //
; // http://www.graphical-installer.com                    //
; // customers@unsigned.sk                                 //
; // All Rights Reserved.                                  //
; ///////////////////////////////////////////////////////////
 
; *********************************************
; * This file contains setting for graphical  *
; * interface. Modify them freerly.           *
; ********************************************* 
 
; Script generated with Graphical Installer Wizard.
 
; UI file for Graphical Installer
#define public GraphicalInstallerUI "GraphicalInstallerUI"
 
; Texts colors
#define public TextsColor    "$000000"
#define public HeadersColor  "$FFFFFF"
#define public InversedColor "$000000"
 
; Buttons colors
#define public ButtonNormalColor   "$FFFFFF"
#define public ButtonFocusedColor  "$FFFFFF"
#define public ButtonPressedColor  "$FFFFFF"
#define public ButtonDisabledColor "$FFFFFF"
 
; Images - all files must be in the same directory as this .iss file!
#define public TopPicture    "Aunoty-top.png"    ; 690x416 px
#define public InnerPicture  "Aunoty-inner.png"  ; 413x237 px
#define public BottomPicture "Aunoty-bottom.png" ; 690x83 px
#define public ButtonPicture "button.png" ; 80x136 px
 
; File with core functions and procedures
#include "compiler:Graphical Installer\GraphicalInstaller_functions.iss"
  

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
; Directive "WizardSmallImageBackColor" was modified for purposes of Graphical Installer.
WizardSmallImageBackColor={#GraphicalInstallerUI}

[Files]
; Pictures with skin 
Source: {#TopPicture};    Flags: dontcopy;
Source: {#InnerPicture};  Flags: dontcopy;
Source: {#BottomPicture}; Flags: dontcopy;
Source: {#ButtonPicture}; Flags: dontcopy;
; DLLs
Source: compiler:Graphical Installer\InnoCallback.dll; Flags: dontcopy;
Source: compiler:Graphical Installer\botva2.dll;       Flags: dontcopy;
 
; End of file (EOF)
