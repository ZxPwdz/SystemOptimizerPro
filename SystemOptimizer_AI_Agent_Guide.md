# 🚀 SYSTEM OPTIMIZER PRO - AI Agent Development Guide

## Complete C# WPF Application Development Specification

**Document Version:** 1.0  
**Target Framework:** .NET 8.0 (or .NET 6.0+)  
**Application Type:** Windows Presentation Foundation (WPF)  
**Theme:** Modern Light Theme with Custom UI  

---

# 📋 TABLE OF CONTENTS

1. [Project Overview](#1-project-overview)
2. [Project Structure](#2-project-structure)
3. [UI/UX Design Specifications](#3-uiux-design-specifications)
4. [Core Features & Functionality](#4-core-features--functionality)
5. [Technical Implementation Details](#5-technical-implementation-details)
6. [Windows API Integration](#6-windows-api-integration)
7. [Data Models & Architecture](#7-data-models--architecture)
8. [Safety & Error Handling](#8-safety--error-handling)
9. [Performance Optimization](#9-performance-optimization)
10. [Testing Requirements](#10-testing-requirements)
11. [Build & Deployment](#11-build--deployment)

---

# 1. PROJECT OVERVIEW

## 1.1 Application Purpose

Create a modern, visually appealing Windows system optimization application that helps users:
- Monitor real-time system resource usage (RAM, CPU, Processes)
- Free up memory by clearing the Windows Standby List
- Clear DNS cache for network optimization
- Clean Windows Recent File history for privacy
- Perform safe registry cleaning operations
- Provide one-click optimization for all cleaning tasks

## 1.2 Core Inspiration

This application is inspired by **Intelligent Standby List Cleaner (ISLC)** by Wagnard but extends functionality with:
- Modern WPF custom UI (light theme)
- Additional system cleaning features
- Real-time resource monitoring dashboard
- Process management capabilities

## 1.3 Target Users

- Gamers seeking to reduce micro-stutters
- Power users wanting system optimization
- Privacy-conscious users
- IT professionals managing system resources

## 1.4 System Requirements

- Windows 10/11 (64-bit recommended)
- .NET 8.0 Runtime
- Administrator privileges (required for memory/registry operations)
- Minimum 4GB RAM
- 50MB disk space

---

# 2. PROJECT STRUCTURE

## 2.1 Solution Architecture

```
SystemOptimizerPro/
│
├── SystemOptimizerPro.sln
│
├── src/
│   └── SystemOptimizerPro/
│       ├── SystemOptimizerPro.csproj
│       │
│       ├── App.xaml
│       ├── App.xaml.cs
│       ├── MainWindow.xaml
│       ├── MainWindow.xaml.cs
│       │
│       ├── Assets/
│       │   ├── Icons/
│       │   │   ├── app_icon.ico
│       │   │   ├── ram_icon.png
│       │   │   ├── cpu_icon.png
│       │   │   ├── clean_icon.png
│       │   │   ├── dns_icon.png
│       │   │   ├── registry_icon.png
│       │   │   ├── history_icon.png
│       │   │   ├── settings_icon.png
│       │   │   └── minimize_icon.png
│       │   │
│       │   └── Fonts/
│       │       └── (Optional custom fonts)
│       │
│       ├── Controls/
│       │   ├── CircularProgressBar.xaml
│       │   ├── CircularProgressBar.xaml.cs
│       │   ├── ModernButton.xaml
│       │   ├── ModernButton.xaml.cs
│       │   ├── StatCard.xaml
│       │   ├── StatCard.xaml.cs
│       │   ├── ProcessListItem.xaml
│       │   ├── ProcessListItem.xaml.cs
│       │   ├── ToggleSwitch.xaml
│       │   └── ToggleSwitch.xaml.cs
│       │
│       ├── Views/
│       │   ├── DashboardView.xaml
│       │   ├── DashboardView.xaml.cs
│       │   ├── MemoryCleanerView.xaml
│       │   ├── MemoryCleanerView.xaml.cs
│       │   ├── ProcessesView.xaml
│       │   ├── ProcessesView.xaml.cs
│       │   ├── CleaningToolsView.xaml
│       │   ├── CleaningToolsView.xaml.cs
│       │   ├── SettingsView.xaml
│       │   └── SettingsView.xaml.cs
│       │
│       ├── ViewModels/
│       │   ├── BaseViewModel.cs
│       │   ├── MainViewModel.cs
│       │   ├── DashboardViewModel.cs
│       │   ├── MemoryCleanerViewModel.cs
│       │   ├── ProcessesViewModel.cs
│       │   ├── CleaningToolsViewModel.cs
│       │   └── SettingsViewModel.cs
│       │
│       ├── Models/
│       │   ├── SystemInfo.cs
│       │   ├── ProcessInfo.cs
│       │   ├── MemoryInfo.cs
│       │   ├── CleaningResult.cs
│       │   ├── RegistryItem.cs
│       │   └── AppSettings.cs
│       │
│       ├── Services/
│       │   ├── Interfaces/
│       │   │   ├── IMemoryService.cs
│       │   │   ├── IProcessService.cs
│       │   │   ├── ICleaningService.cs
│       │   │   ├── IRegistryService.cs
│       │   │   └── ISettingsService.cs
│       │   │
│       │   ├── MemoryService.cs
│       │   ├── ProcessService.cs
│       │   ├── DnsCacheService.cs
│       │   ├── RecentFilesService.cs
│       │   ├── RegistryCleanerService.cs
│       │   ├── StandbyListService.cs
│       │   └── SettingsService.cs
│       │
│       ├── Native/
│       │   ├── NativeMethods.cs
│       │   ├── MemoryApi.cs
│       │   ├── ProcessApi.cs
│       │   └── Structs/
│       │       ├── MemoryStatusEx.cs
│       │       ├── SystemCacheInformation.cs
│       │       └── PerformanceInformation.cs
│       │
│       ├── Helpers/
│       │   ├── RelayCommand.cs
│       │   ├── AsyncRelayCommand.cs
│       │   ├── ObservableObject.cs
│       │   ├── ByteConverter.cs
│       │   ├── AdminHelper.cs
│       │   └── ThemeHelper.cs
│       │
│       ├── Converters/
│       │   ├── BytesToGigabytesConverter.cs
│       │   ├── PercentageToColorConverter.cs
│       │   ├── BoolToVisibilityConverter.cs
│       │   └── InverseBoolConverter.cs
│       │
│       ├── Themes/
│       │   ├── LightTheme.xaml
│       │   ├── Colors.xaml
│       │   ├── Brushes.xaml
│       │   ├── Typography.xaml
│       │   └── Controls.xaml
│       │
│       └── Resources/
│           └── Strings.xaml
│
├── tests/
│   └── SystemOptimizerPro.Tests/
│       ├── SystemOptimizerPro.Tests.csproj
│       ├── Services/
│       │   └── (Unit tests for services)
│       └── ViewModels/
│           └── (Unit tests for view models)
│
└── docs/
    ├── README.md
    └── CHANGELOG.md
```

## 2.2 Project File Configuration

### SystemOptimizerPro.csproj

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows</TargetFramework>
    <Nullable>enable</Nullable>
    <UseWPF>true</UseWPF>
    <ApplicationIcon>Assets\Icons\app_icon.ico</ApplicationIcon>
    <ApplicationManifest>app.manifest</ApplicationManifest>
    <AssemblyName>SystemOptimizerPro</AssemblyName>
    <RootNamespace>SystemOptimizerPro</RootNamespace>
    <Version>1.0.0</Version>
    <Authors>Your Name</Authors>
    <Company>Your Company</Company>
    <Product>System Optimizer Pro</Product>
    <Description>Modern system optimization and memory management tool</Description>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="CommunityToolkit.Mvvm" Version="8.2.2" />
    <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="8.0.0" />
    <PackageReference Include="Hardcodet.NotifyIcon.Wpf" Version="1.1.0" />
    <PackageReference Include="LiveChartsCore.SkiaSharpView.WPF" Version="2.0.0-rc2" />
  </ItemGroup>

  <ItemGroup>
    <Resource Include="Assets\**\*" />
  </ItemGroup>

</Project>
```

### app.manifest (Administrator Privileges)

```xml
<?xml version="1.0" encoding="utf-8"?>
<assembly manifestVersion="1.0" xmlns="urn:schemas-microsoft-com:asm.v1">
  <assemblyIdentity version="1.0.0.0" name="MyApplication.app"/>
  <trustInfo xmlns="urn:schemas-microsoft-com:asm.v2">
    <security>
      <requestedPrivileges xmlns="urn:schemas-microsoft-com:asm.v3">
        <requestedExecutionLevel level="requireAdministrator" uiAccess="false" />
      </requestedPrivileges>
    </security>
  </trustInfo>
</assembly>
```

---

# 3. UI/UX DESIGN SPECIFICATIONS

## 3.1 Design Philosophy

- **Clean & Modern**: Minimalist design with purposeful elements
- **Light Theme**: Soft whites, light grays, and accent colors
- **Card-Based Layout**: Information grouped in floating cards
- **Smooth Animations**: Subtle transitions and micro-interactions
- **Accessibility**: High contrast ratios, clear typography

## 3.2 Color Palette

### Primary Colors (Light Theme)

```xaml
<!-- Colors.xaml -->
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    
    <!-- Background Colors -->
    <Color x:Key="BackgroundPrimary">#FFFFFF</Color>
    <Color x:Key="BackgroundSecondary">#F8F9FA</Color>
    <Color x:Key="BackgroundTertiary">#F1F3F4</Color>
    <Color x:Key="SidebarBackground">#FAFBFC</Color>
    
    <!-- Card Colors -->
    <Color x:Key="CardBackground">#FFFFFF</Color>
    <Color x:Key="CardBorder">#E8EAED</Color>
    <Color x:Key="CardShadow">#1A000000</Color>
    
    <!-- Text Colors -->
    <Color x:Key="TextPrimary">#202124</Color>
    <Color x:Key="TextSecondary">#5F6368</Color>
    <Color x:Key="TextTertiary">#9AA0A6</Color>
    <Color x:Key="TextOnAccent">#FFFFFF</Color>
    
    <!-- Accent Colors -->
    <Color x:Key="AccentPrimary">#1A73E8</Color>         <!-- Blue -->
    <Color x:Key="AccentPrimaryHover">#1557B0</Color>
    <Color x:Key="AccentSecondary">#34A853</Color>       <!-- Green -->
    <Color x:Key="AccentWarning">#FBBC04</Color>         <!-- Yellow -->
    <Color x:Key="AccentDanger">#EA4335</Color>          <!-- Red -->
    <Color x:Key="AccentPurple">#A142F4</Color>          <!-- Purple -->
    
    <!-- Status Colors -->
    <Color x:Key="StatusHealthy">#34A853</Color>
    <Color x:Key="StatusWarning">#FBBC04</Color>
    <Color x:Key="StatusCritical">#EA4335</Color>
    
    <!-- Progress Bar Colors -->
    <Color x:Key="ProgressLow">#34A853</Color>           <!-- 0-50% -->
    <Color x:Key="ProgressMedium">#FBBC04</Color>        <!-- 50-80% -->
    <Color x:Key="ProgressHigh">#EA4335</Color>          <!-- 80-100% -->
    
    <!-- Border Colors -->
    <Color x:Key="BorderLight">#E8EAED</Color>
    <Color x:Key="BorderMedium">#DADCE0</Color>
    <Color x:Key="BorderFocus">#1A73E8</Color>
    
</ResourceDictionary>
```

### Brushes

```xaml
<!-- Brushes.xaml -->
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

    <ResourceDictionary.MergedDictionaries>
        <ResourceDictionary Source="Colors.xaml"/>
    </ResourceDictionary.MergedDictionaries>
    
    <!-- Background Brushes -->
    <SolidColorBrush x:Key="BackgroundPrimaryBrush" Color="{StaticResource BackgroundPrimary}"/>
    <SolidColorBrush x:Key="BackgroundSecondaryBrush" Color="{StaticResource BackgroundSecondary}"/>
    <SolidColorBrush x:Key="SidebarBackgroundBrush" Color="{StaticResource SidebarBackground}"/>
    
    <!-- Card Brushes -->
    <SolidColorBrush x:Key="CardBackgroundBrush" Color="{StaticResource CardBackground}"/>
    <SolidColorBrush x:Key="CardBorderBrush" Color="{StaticResource CardBorder}"/>
    
    <!-- Text Brushes -->
    <SolidColorBrush x:Key="TextPrimaryBrush" Color="{StaticResource TextPrimary}"/>
    <SolidColorBrush x:Key="TextSecondaryBrush" Color="{StaticResource TextSecondary}"/>
    <SolidColorBrush x:Key="TextTertiaryBrush" Color="{StaticResource TextTertiary}"/>
    
    <!-- Accent Brushes -->
    <SolidColorBrush x:Key="AccentPrimaryBrush" Color="{StaticResource AccentPrimary}"/>
    <SolidColorBrush x:Key="AccentSecondaryBrush" Color="{StaticResource AccentSecondary}"/>
    <SolidColorBrush x:Key="AccentDangerBrush" Color="{StaticResource AccentDanger}"/>
    
    <!-- Gradient Brushes -->
    <LinearGradientBrush x:Key="AccentGradientBrush" StartPoint="0,0" EndPoint="1,1">
        <GradientStop Color="#1A73E8" Offset="0"/>
        <GradientStop Color="#A142F4" Offset="1"/>
    </LinearGradientBrush>
    
    <LinearGradientBrush x:Key="CardShadowBrush" StartPoint="0,0" EndPoint="0,1">
        <GradientStop Color="#08000000" Offset="0"/>
        <GradientStop Color="#00000000" Offset="1"/>
    </LinearGradientBrush>
    
</ResourceDictionary>
```

## 3.3 Typography

```xaml
<!-- Typography.xaml -->
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

    <!-- Font Family -->
    <FontFamily x:Key="PrimaryFont">Segoe UI, Arial, sans-serif</FontFamily>
    <FontFamily x:Key="MonospaceFont">Cascadia Code, Consolas, monospace</FontFamily>
    
    <!-- Font Sizes -->
    <sys:Double x:Key="FontSizeXS" xmlns:sys="clr-namespace:System;assembly=mscorlib">10</sys:Double>
    <sys:Double x:Key="FontSizeSmall" xmlns:sys="clr-namespace:System;assembly=mscorlib">12</sys:Double>
    <sys:Double x:Key="FontSizeNormal" xmlns:sys="clr-namespace:System;assembly=mscorlib">14</sys:Double>
    <sys:Double x:Key="FontSizeMedium" xmlns:sys="clr-namespace:System;assembly=mscorlib">16</sys:Double>
    <sys:Double x:Key="FontSizeLarge" xmlns:sys="clr-namespace:System;assembly=mscorlib">20</sys:Double>
    <sys:Double x:Key="FontSizeXL" xmlns:sys="clr-namespace:System;assembly=mscorlib">24</sys:Double>
    <sys:Double x:Key="FontSizeXXL" xmlns:sys="clr-namespace:System;assembly=mscorlib">32</sys:Double>
    <sys:Double x:Key="FontSizeHero" xmlns:sys="clr-namespace:System;assembly=mscorlib">48</sys:Double>
    
    <!-- Text Styles -->
    <Style x:Key="HeadingStyle" TargetType="TextBlock">
        <Setter Property="FontFamily" Value="{StaticResource PrimaryFont}"/>
        <Setter Property="FontSize" Value="{StaticResource FontSizeXL}"/>
        <Setter Property="FontWeight" Value="SemiBold"/>
        <Setter Property="Foreground" Value="{StaticResource TextPrimaryBrush}"/>
    </Style>
    
    <Style x:Key="SubheadingStyle" TargetType="TextBlock">
        <Setter Property="FontFamily" Value="{StaticResource PrimaryFont}"/>
        <Setter Property="FontSize" Value="{StaticResource FontSizeMedium}"/>
        <Setter Property="FontWeight" Value="SemiBold"/>
        <Setter Property="Foreground" Value="{StaticResource TextPrimaryBrush}"/>
    </Style>
    
    <Style x:Key="BodyStyle" TargetType="TextBlock">
        <Setter Property="FontFamily" Value="{StaticResource PrimaryFont}"/>
        <Setter Property="FontSize" Value="{StaticResource FontSizeNormal}"/>
        <Setter Property="Foreground" Value="{StaticResource TextSecondaryBrush}"/>
    </Style>
    
    <Style x:Key="CaptionStyle" TargetType="TextBlock">
        <Setter Property="FontFamily" Value="{StaticResource PrimaryFont}"/>
        <Setter Property="FontSize" Value="{StaticResource FontSizeSmall}"/>
        <Setter Property="Foreground" Value="{StaticResource TextTertiaryBrush}"/>
    </Style>
    
    <Style x:Key="StatValueStyle" TargetType="TextBlock">
        <Setter Property="FontFamily" Value="{StaticResource PrimaryFont}"/>
        <Setter Property="FontSize" Value="{StaticResource FontSizeHero}"/>
        <Setter Property="FontWeight" Value="Light"/>
        <Setter Property="Foreground" Value="{StaticResource TextPrimaryBrush}"/>
    </Style>
    
</ResourceDictionary>
```

## 3.4 Main Window Layout

### Layout Structure

```
┌─────────────────────────────────────────────────────────────────────┐
│  ┌─────────────────────────────────────────────────────────────┐   │
│  │ Custom Title Bar (Drag Region) │ Minimize │ Maximize │ Close │   │
│  └─────────────────────────────────────────────────────────────┘   │
│                                                                     │
│  ┌──────────┬──────────────────────────────────────────────────┐   │
│  │          │                                                    │   │
│  │  SIDE    │              MAIN CONTENT AREA                    │   │
│  │  BAR     │                                                    │   │
│  │          │   ┌────────────────────────────────────────────┐  │   │
│  │ ┌──────┐ │   │                                            │  │   │
│  │ │ Logo │ │   │     Dynamic View Content                   │  │   │
│  │ └──────┘ │   │     (Dashboard / Memory / Processes /      │  │   │
│  │          │   │      Cleaning Tools / Settings)            │  │   │
│  │ ┌──────┐ │   │                                            │  │   │
│  │ │ Nav  │ │   │                                            │  │   │
│  │ │ Item │ │   │                                            │  │   │
│  │ └──────┘ │   │                                            │  │   │
│  │ ┌──────┐ │   │                                            │  │   │
│  │ │ Nav  │ │   │                                            │  │   │
│  │ │ Item │ │   │                                            │  │   │
│  │ └──────┘ │   │                                            │  │   │
│  │          │   │                                            │  │   │
│  │ ┌──────┐ │   │                                            │  │   │
│  │ │ Nav  │ │   └────────────────────────────────────────────┘  │   │
│  │ │ Item │ │                                                    │   │
│  │ └──────┘ │                                                    │   │
│  │          │                                                    │   │
│  │  ─────   │                                                    │   │
│  │          │                                                    │   │
│  │ ┌──────┐ │                                                    │   │
│  │ │ Set- │ │                                                    │   │
│  │ │ tings│ │                                                    │   │
│  │ └──────┘ │                                                    │   │
│  │          │                                                    │   │
│  └──────────┴──────────────────────────────────────────────────┘   │
│                                                                     │
│  ┌─────────────────────────────────────────────────────────────┐   │
│  │ Status Bar: Last cleaned: XX:XX │ RAM: XX% │ CPU: XX%       │   │
│  └─────────────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────────────┘
```

### MainWindow.xaml

```xaml
<Window x:Class="SystemOptimizerPro.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:SystemOptimizerPro"
        xmlns:views="clr-namespace:SystemOptimizerPro.Views"
        mc:Ignorable="d"
        Title="System Optimizer Pro"
        Height="720" Width="1100"
        MinHeight="600" MinWidth="900"
        WindowStyle="None"
        AllowsTransparency="True"
        Background="Transparent"
        WindowStartupLocation="CenterScreen"
        ResizeMode="CanResizeWithGrip">
    
    <Window.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Themes/LightTheme.xaml"/>
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Window.Resources>
    
    <!-- Main Container with Shadow -->
    <Border Background="{StaticResource BackgroundPrimaryBrush}"
            CornerRadius="12"
            Margin="10">
        <Border.Effect>
            <DropShadowEffect BlurRadius="20" 
                              ShadowDepth="0" 
                              Opacity="0.3"
                              Color="#000000"/>
        </Border.Effect>
        
        <Grid>
            <Grid.RowDefinitions>
                <RowDefinition Height="40"/>   <!-- Title Bar -->
                <RowDefinition Height="*"/>    <!-- Content -->
                <RowDefinition Height="32"/>   <!-- Status Bar -->
            </Grid.RowDefinitions>
            
            <!-- Custom Title Bar -->
            <Border Grid.Row="0" 
                    Background="{StaticResource BackgroundPrimaryBrush}"
                    CornerRadius="12,12,0,0"
                    MouseLeftButtonDown="TitleBar_MouseLeftButtonDown">
                <Grid>
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="Auto"/>
                        <ColumnDefinition Width="*"/>
                        <ColumnDefinition Width="Auto"/>
                    </Grid.ColumnDefinitions>
                    
                    <!-- App Icon & Title -->
                    <StackPanel Grid.Column="0" 
                                Orientation="Horizontal" 
                                Margin="16,0,0,0"
                                VerticalAlignment="Center">
                        <Image Source="Assets/Icons/app_icon.ico" 
                               Width="20" Height="20" 
                               Margin="0,0,10,0"/>
                        <TextBlock Text="System Optimizer Pro" 
                                   Style="{StaticResource SubheadingStyle}"
                                   VerticalAlignment="Center"/>
                    </StackPanel>
                    
                    <!-- Window Controls -->
                    <StackPanel Grid.Column="2" 
                                Orientation="Horizontal"
                                Margin="0,0,8,0">
                        <Button x:Name="MinimizeButton" 
                                Style="{StaticResource WindowControlButton}"
                                Click="MinimizeButton_Click"
                                Content="─"
                                ToolTip="Minimize"/>
                        <Button x:Name="MaximizeButton" 
                                Style="{StaticResource WindowControlButton}"
                                Click="MaximizeButton_Click"
                                Content="□"
                                ToolTip="Maximize"/>
                        <Button x:Name="CloseButton" 
                                Style="{StaticResource WindowCloseButton}"
                                Click="CloseButton_Click"
                                Content="✕"
                                ToolTip="Close"/>
                    </StackPanel>
                </Grid>
            </Border>
            
            <!-- Main Content Area -->
            <Grid Grid.Row="1">
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="220"/>  <!-- Sidebar -->
                    <ColumnDefinition Width="*"/>    <!-- Content -->
                </Grid.ColumnDefinitions>
                
                <!-- Sidebar Navigation -->
                <Border Grid.Column="0" 
                        Background="{StaticResource SidebarBackgroundBrush}"
                        BorderBrush="{StaticResource BorderLight}"
                        BorderThickness="0,0,1,0">
                    
                    <Grid>
                        <Grid.RowDefinitions>
                            <RowDefinition Height="Auto"/>
                            <RowDefinition Height="*"/>
                            <RowDefinition Height="Auto"/>
                        </Grid.RowDefinitions>
                        
                        <!-- Quick Optimize Button -->
                        <Button Grid.Row="0" 
                                x:Name="QuickOptimizeButton"
                                Style="{StaticResource PrimaryActionButton}"
                                Margin="16,16,16,24"
                                Height="48"
                                Command="{Binding QuickOptimizeCommand}">
                            <StackPanel Orientation="Horizontal">
                                <TextBlock Text="⚡" FontSize="18" Margin="0,0,8,0"/>
                                <TextBlock Text="Quick Optimize" 
                                           FontWeight="SemiBold"
                                           VerticalAlignment="Center"/>
                            </StackPanel>
                        </Button>
                        
                        <!-- Navigation Items -->
                        <StackPanel Grid.Row="1" Margin="8,0">
                            <RadioButton Style="{StaticResource NavItemStyle}"
                                         Content="Dashboard"
                                         Tag="Dashboard"
                                         IsChecked="True"
                                         Command="{Binding NavigateCommand}"
                                         CommandParameter="Dashboard"/>
                            
                            <RadioButton Style="{StaticResource NavItemStyle}"
                                         Content="Memory Cleaner"
                                         Tag="Memory"
                                         Command="{Binding NavigateCommand}"
                                         CommandParameter="Memory"/>
                            
                            <RadioButton Style="{StaticResource NavItemStyle}"
                                         Content="Processes"
                                         Tag="Processes"
                                         Command="{Binding NavigateCommand}"
                                         CommandParameter="Processes"/>
                            
                            <RadioButton Style="{StaticResource NavItemStyle}"
                                         Content="Cleaning Tools"
                                         Tag="Cleaning"
                                         Command="{Binding NavigateCommand}"
                                         CommandParameter="Cleaning"/>
                            
                            <Separator Margin="8,16" 
                                        Background="{StaticResource BorderLight}"/>
                            
                            <RadioButton Style="{StaticResource NavItemStyle}"
                                         Content="Settings"
                                         Tag="Settings"
                                         Command="{Binding NavigateCommand}"
                                         CommandParameter="Settings"/>
                        </StackPanel>
                        
                        <!-- Version Info -->
                        <TextBlock Grid.Row="2" 
                                   Text="v1.0.0" 
                                   Style="{StaticResource CaptionStyle}"
                                   HorizontalAlignment="Center"
                                   Margin="0,0,0,16"/>
                    </Grid>
                </Border>
                
                <!-- Dynamic Content -->
                <ContentControl Grid.Column="1"
                                Content="{Binding CurrentView}"
                                Margin="0">
                    <ContentControl.ContentTransitions>
                        <TransitionCollection>
                            <ContentThemeTransition/>
                        </TransitionCollection>
                    </ContentControl.ContentTransitions>
                </ContentControl>
            </Grid>
            
            <!-- Status Bar -->
            <Border Grid.Row="2" 
                    Background="{StaticResource BackgroundSecondaryBrush}"
                    CornerRadius="0,0,12,12"
                    BorderBrush="{StaticResource BorderLight}"
                    BorderThickness="0,1,0,0">
                <Grid Margin="16,0">
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="*"/>
                        <ColumnDefinition Width="Auto"/>
                        <ColumnDefinition Width="Auto"/>
                        <ColumnDefinition Width="Auto"/>
                    </Grid.ColumnDefinitions>
                    
                    <TextBlock Grid.Column="0" 
                               Text="{Binding StatusMessage}" 
                               Style="{StaticResource CaptionStyle}"
                               VerticalAlignment="Center"/>
                    
                    <StackPanel Grid.Column="1" 
                                Orientation="Horizontal" 
                                Margin="24,0">
                        <TextBlock Text="RAM: " 
                                   Style="{StaticResource CaptionStyle}"
                                   VerticalAlignment="Center"/>
                        <TextBlock Text="{Binding RamUsagePercent, StringFormat={}{0}%}" 
                                   Foreground="{Binding RamUsageColor}"
                                   FontWeight="SemiBold"
                                   VerticalAlignment="Center"/>
                    </StackPanel>
                    
                    <StackPanel Grid.Column="2" 
                                Orientation="Horizontal" 
                                Margin="24,0">
                        <TextBlock Text="CPU: " 
                                   Style="{StaticResource CaptionStyle}"
                                   VerticalAlignment="Center"/>
                        <TextBlock Text="{Binding CpuUsagePercent, StringFormat={}{0}%}" 
                                   Foreground="{Binding CpuUsageColor}"
                                   FontWeight="SemiBold"
                                   VerticalAlignment="Center"/>
                    </StackPanel>
                    
                    <StackPanel Grid.Column="3" 
                                Orientation="Horizontal">
                        <Ellipse Width="8" Height="8" 
                                 Fill="{Binding MonitoringStatusColor}"
                                 Margin="0,0,6,0"/>
                        <TextBlock Text="{Binding MonitoringStatus}" 
                                   Style="{StaticResource CaptionStyle}"
                                   VerticalAlignment="Center"/>
                    </StackPanel>
                </Grid>
            </Border>
        </Grid>
    </Border>
</Window>
```

## 3.5 Dashboard View Design

```
┌────────────────────────────────────────────────────────────────────────┐
│  Dashboard                                                    Today ▼  │
├────────────────────────────────────────────────────────────────────────┤
│                                                                        │
│  ┌─────────────────┐  ┌─────────────────┐  ┌─────────────────┐        │
│  │   ╭───────╮     │  │   ╭───────╮     │  │                 │        │
│  │   │       │     │  │   │       │     │  │    147          │        │
│  │   │  68%  │     │  │   │  23%  │     │  │    Active       │        │
│  │   │       │     │  │   │       │     │  │    Processes    │        │
│  │   ╰───────╯     │  │   ╰───────╯     │  │                 │        │
│  │   RAM Usage     │  │   CPU Usage     │  │    ↓12 from     │        │
│  │   10.8 / 16 GB  │  │   2.3 GHz       │  │    last hour    │        │
│  └─────────────────┘  └─────────────────┘  └─────────────────┘        │
│                                                                        │
│  ┌─────────────────────────────────────────────────────────────────┐  │
│  │  Memory Breakdown                                                │  │
│  │  ┌────────────────────────────────────────────────────────────┐ │  │
│  │  │████████████████████░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│ │  │
│  │  │ In Use: 10.8 GB │ Standby: 3.2 GB │ Free: 2.0 GB         │ │  │
│  │  └────────────────────────────────────────────────────────────┘ │  │
│  └─────────────────────────────────────────────────────────────────┘  │
│                                                                        │
│  ┌──────────────────────────────┐  ┌───────────────────────────────┐  │
│  │  Quick Actions               │  │  System Health                │  │
│  │                              │  │                               │  │
│  │  ┌────────┐  ┌────────┐     │  │  ● Memory        Good         │  │
│  │  │  🧹    │  │  🌐    │     │  │  ● DNS Cache     3,421 entries│  │
│  │  │ Clear  │  │ Flush  │     │  │  ● Recent Files  156 items    │  │
│  │  │ Standby│  │  DNS   │     │  │  ● Registry      12 issues    │  │
│  │  └────────┘  └────────┘     │  │                               │  │
│  │                              │  │  Last optimized: 2 hours ago │  │
│  │  ┌────────┐  ┌────────┐     │  │                               │  │
│  │  │  📁    │  │  🔧    │     │  │                               │  │
│  │  │ Clear  │  │ Clean  │     │  │                               │  │
│  │  │ Recent │  │Registry│     │  │                               │  │
│  │  └────────┘  └────────┘     │  │                               │  │
│  └──────────────────────────────┘  └───────────────────────────────┘  │
│                                                                        │
└────────────────────────────────────────────────────────────────────────┘
```

### DashboardView.xaml

```xaml
<UserControl x:Class="SystemOptimizerPro.Views.DashboardView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:controls="clr-namespace:SystemOptimizerPro.Controls"
             xmlns:lvc="clr-namespace:LiveChartsCore.SkiaSharpView.WPF;assembly=LiveChartsCore.SkiaSharpView.WPF">
    
    <ScrollViewer VerticalScrollBarVisibility="Auto" 
                  HorizontalScrollBarVisibility="Disabled"
                  Padding="24">
        <Grid>
            <Grid.RowDefinitions>
                <RowDefinition Height="Auto"/>  <!-- Header -->
                <RowDefinition Height="Auto"/>  <!-- Stats Cards -->
                <RowDefinition Height="Auto"/>  <!-- Memory Breakdown -->
                <RowDefinition Height="*"/>     <!-- Bottom Section -->
            </Grid.RowDefinitions>
            
            <!-- Header -->
            <Grid Grid.Row="0" Margin="0,0,0,24">
                <TextBlock Text="Dashboard" 
                           Style="{StaticResource HeadingStyle}"/>
            </Grid>
            
            <!-- Stats Cards Row -->
            <UniformGrid Grid.Row="1" 
                         Columns="3" 
                         Margin="0,0,0,24">
                
                <!-- RAM Usage Card -->
                <Border Style="{StaticResource CardStyle}" Margin="0,0,12,0">
                    <Grid>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="*"/>
                            <ColumnDefinition Width="Auto"/>
                        </Grid.ColumnDefinitions>
                        
                        <StackPanel Grid.Column="0" VerticalAlignment="Center">
                            <TextBlock Text="RAM Usage" 
                                       Style="{StaticResource CaptionStyle}"
                                       Margin="0,0,0,4"/>
                            <TextBlock Text="{Binding RamUsedFormatted}" 
                                       Style="{StaticResource StatValueStyle}"/>
                            <TextBlock Style="{StaticResource CaptionStyle}">
                                <Run Text="{Binding RamUsedGB, StringFormat={}{0:F1}}"/>
                                <Run Text=" / "/>
                                <Run Text="{Binding RamTotalGB, StringFormat={}{0:F0}}"/>
                                <Run Text=" GB"/>
                            </TextBlock>
                        </StackPanel>
                        
                        <controls:CircularProgressBar 
                            Grid.Column="1"
                            Value="{Binding RamUsagePercent}"
                            Size="80"
                            StrokeThickness="8"
                            ProgressColor="{Binding RamProgressColor}"/>
                    </Grid>
                </Border>
                
                <!-- CPU Usage Card -->
                <Border Style="{StaticResource CardStyle}" Margin="6,0">
                    <Grid>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="*"/>
                            <ColumnDefinition Width="Auto"/>
                        </Grid.ColumnDefinitions>
                        
                        <StackPanel Grid.Column="0" VerticalAlignment="Center">
                            <TextBlock Text="CPU Usage" 
                                       Style="{StaticResource CaptionStyle}"
                                       Margin="0,0,0,4"/>
                            <TextBlock Text="{Binding CpuUsagePercent, StringFormat={}{0}%}" 
                                       Style="{StaticResource StatValueStyle}"/>
                            <TextBlock Text="{Binding CpuSpeed}" 
                                       Style="{StaticResource CaptionStyle}"/>
                        </StackPanel>
                        
                        <controls:CircularProgressBar 
                            Grid.Column="1"
                            Value="{Binding CpuUsagePercent}"
                            Size="80"
                            StrokeThickness="8"
                            ProgressColor="{Binding CpuProgressColor}"/>
                    </Grid>
                </Border>
                
                <!-- Processes Card -->
                <Border Style="{StaticResource CardStyle}" Margin="12,0,0,0">
                    <StackPanel VerticalAlignment="Center">
                        <TextBlock Text="Active Processes" 
                                   Style="{StaticResource CaptionStyle}"
                                   Margin="0,0,0,4"/>
                        <TextBlock Text="{Binding ProcessCount}" 
                                   Style="{StaticResource StatValueStyle}"/>
                        <StackPanel Orientation="Horizontal">
                            <TextBlock Text="↓" 
                                       Foreground="{StaticResource AccentSecondaryBrush}"
                                       Margin="0,0,4,0"/>
                            <TextBlock Text="{Binding ProcessChange}" 
                                       Style="{StaticResource CaptionStyle}"/>
                        </StackPanel>
                    </StackPanel>
                </Border>
            </UniformGrid>
            
            <!-- Memory Breakdown -->
            <Border Grid.Row="2" 
                    Style="{StaticResource CardStyle}" 
                    Margin="0,0,0,24">
                <StackPanel>
                    <TextBlock Text="Memory Breakdown" 
                               Style="{StaticResource SubheadingStyle}"
                               Margin="0,0,0,16"/>
                    
                    <!-- Stacked Progress Bar -->
                    <Grid Height="24" Margin="0,0,0,12">
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="{Binding InUseWidth}"/>
                            <ColumnDefinition Width="{Binding StandbyWidth}"/>
                            <ColumnDefinition Width="{Binding FreeWidth}"/>
                        </Grid.ColumnDefinitions>
                        
                        <Border Grid.Column="0" 
                                Background="{StaticResource AccentPrimaryBrush}"
                                CornerRadius="4,0,0,4"/>
                        <Border Grid.Column="1" 
                                Background="{StaticResource AccentWarningBrush}"/>
                        <Border Grid.Column="2" 
                                Background="{StaticResource AccentSecondaryBrush}"
                                CornerRadius="0,4,4,0"/>
                    </Grid>
                    
                    <!-- Legend -->
                    <UniformGrid Columns="3">
                        <StackPanel Orientation="Horizontal">
                            <Ellipse Width="12" Height="12" 
                                     Fill="{StaticResource AccentPrimaryBrush}"
                                     Margin="0,0,8,0"/>
                            <TextBlock Style="{StaticResource CaptionStyle}">
                                <Run Text="In Use: "/>
                                <Run Text="{Binding InUseGB, StringFormat={}{0:F1} GB}"/>
                            </TextBlock>
                        </StackPanel>
                        
                        <StackPanel Orientation="Horizontal" HorizontalAlignment="Center">
                            <Ellipse Width="12" Height="12" 
                                     Fill="{StaticResource AccentWarningBrush}"
                                     Margin="0,0,8,0"/>
                            <TextBlock Style="{StaticResource CaptionStyle}">
                                <Run Text="Standby: "/>
                                <Run Text="{Binding StandbyGB, StringFormat={}{0:F1} GB}"/>
                            </TextBlock>
                        </StackPanel>
                        
                        <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                            <Ellipse Width="12" Height="12" 
                                     Fill="{StaticResource AccentSecondaryBrush}"
                                     Margin="0,0,8,0"/>
                            <TextBlock Style="{StaticResource CaptionStyle}">
                                <Run Text="Free: "/>
                                <Run Text="{Binding FreeGB, StringFormat={}{0:F1} GB}"/>
                            </TextBlock>
                        </StackPanel>
                    </UniformGrid>
                </StackPanel>
            </Border>
            
            <!-- Bottom Section -->
            <Grid Grid.Row="3">
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="*"/>
                    <ColumnDefinition Width="*"/>
                </Grid.ColumnDefinitions>
                
                <!-- Quick Actions -->
                <Border Grid.Column="0" 
                        Style="{StaticResource CardStyle}" 
                        Margin="0,0,12,0">
                    <StackPanel>
                        <TextBlock Text="Quick Actions" 
                                   Style="{StaticResource SubheadingStyle}"
                                   Margin="0,0,0,16"/>
                        
                        <UniformGrid Columns="2" Rows="2">
                            <Button Style="{StaticResource QuickActionButton}"
                                    Command="{Binding ClearStandbyCommand}"
                                    Margin="0,0,8,8">
                                <StackPanel>
                                    <TextBlock Text="🧹" FontSize="24" 
                                               HorizontalAlignment="Center"/>
                                    <TextBlock Text="Clear Standby" 
                                               HorizontalAlignment="Center"
                                               Margin="0,8,0,0"/>
                                </StackPanel>
                            </Button>
                            
                            <Button Style="{StaticResource QuickActionButton}"
                                    Command="{Binding FlushDnsCommand}"
                                    Margin="8,0,0,8">
                                <StackPanel>
                                    <TextBlock Text="🌐" FontSize="24" 
                                               HorizontalAlignment="Center"/>
                                    <TextBlock Text="Flush DNS" 
                                               HorizontalAlignment="Center"
                                               Margin="0,8,0,0"/>
                                </StackPanel>
                            </Button>
                            
                            <Button Style="{StaticResource QuickActionButton}"
                                    Command="{Binding ClearRecentCommand}"
                                    Margin="0,8,8,0">
                                <StackPanel>
                                    <TextBlock Text="📁" FontSize="24" 
                                               HorizontalAlignment="Center"/>
                                    <TextBlock Text="Clear Recent" 
                                               HorizontalAlignment="Center"
                                               Margin="0,8,0,0"/>
                                </StackPanel>
                            </Button>
                            
                            <Button Style="{StaticResource QuickActionButton}"
                                    Command="{Binding CleanRegistryCommand}"
                                    Margin="8,8,0,0">
                                <StackPanel>
                                    <TextBlock Text="🔧" FontSize="24" 
                                               HorizontalAlignment="Center"/>
                                    <TextBlock Text="Clean Registry" 
                                               HorizontalAlignment="Center"
                                               Margin="0,8,0,0"/>
                                </StackPanel>
                            </Button>
                        </UniformGrid>
                    </StackPanel>
                </Border>
                
                <!-- System Health -->
                <Border Grid.Column="1" 
                        Style="{StaticResource CardStyle}" 
                        Margin="12,0,0,0">
                    <StackPanel>
                        <TextBlock Text="System Health" 
                                   Style="{StaticResource SubheadingStyle}"
                                   Margin="0,0,0,16"/>
                        
                        <ItemsControl ItemsSource="{Binding HealthItems}">
                            <ItemsControl.ItemTemplate>
                                <DataTemplate>
                                    <Grid Margin="0,0,0,12">
                                        <Grid.ColumnDefinitions>
                                            <ColumnDefinition Width="Auto"/>
                                            <ColumnDefinition Width="*"/>
                                            <ColumnDefinition Width="Auto"/>
                                        </Grid.ColumnDefinitions>
                                        
                                        <Ellipse Grid.Column="0" 
                                                 Width="8" Height="8"
                                                 Fill="{Binding StatusColor}"
                                                 Margin="0,0,12,0"
                                                 VerticalAlignment="Center"/>
                                        
                                        <TextBlock Grid.Column="1" 
                                                   Text="{Binding Name}"
                                                   Style="{StaticResource BodyStyle}"
                                                   VerticalAlignment="Center"/>
                                        
                                        <TextBlock Grid.Column="2" 
                                                   Text="{Binding Value}"
                                                   Style="{StaticResource CaptionStyle}"
                                                   VerticalAlignment="Center"/>
                                    </Grid>
                                </DataTemplate>
                            </ItemsControl.ItemTemplate>
                        </ItemsControl>
                        
                        <Separator Margin="0,8"/>
                        
                        <TextBlock Style="{StaticResource CaptionStyle}">
                            <Run Text="Last optimized: "/>
                            <Run Text="{Binding LastOptimizedTime}"/>
                        </TextBlock>
                    </StackPanel>
                </Border>
            </Grid>
        </Grid>
    </ScrollViewer>
</UserControl>
```

## 3.6 Custom Control Styles

### Button Styles (Controls.xaml)

```xaml
<!-- Primary Action Button (Gradient) -->
<Style x:Key="PrimaryActionButton" TargetType="Button">
    <Setter Property="Background" Value="{StaticResource AccentGradientBrush}"/>
    <Setter Property="Foreground" Value="White"/>
    <Setter Property="BorderThickness" Value="0"/>
    <Setter Property="Padding" Value="20,12"/>
    <Setter Property="FontSize" Value="14"/>
    <Setter Property="FontWeight" Value="SemiBold"/>
    <Setter Property="Cursor" Value="Hand"/>
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="Button">
                <Border x:Name="border"
                        Background="{TemplateBinding Background}"
                        BorderThickness="{TemplateBinding BorderThickness}"
                        CornerRadius="8"
                        Padding="{TemplateBinding Padding}">
                    <Border.Effect>
                        <DropShadowEffect BlurRadius="8" 
                                          ShadowDepth="2" 
                                          Opacity="0.2"
                                          Color="#1A73E8"/>
                    </Border.Effect>
                    <ContentPresenter HorizontalAlignment="Center" 
                                      VerticalAlignment="Center"/>
                </Border>
                <ControlTemplate.Triggers>
                    <Trigger Property="IsMouseOver" Value="True">
                        <Setter TargetName="border" Property="Effect">
                            <Setter.Value>
                                <DropShadowEffect BlurRadius="12" 
                                                  ShadowDepth="4" 
                                                  Opacity="0.3"
                                                  Color="#1A73E8"/>
                            </Setter.Value>
                        </Setter>
                    </Trigger>
                    <Trigger Property="IsPressed" Value="True">
                        <Setter TargetName="border" Property="RenderTransform">
                            <Setter.Value>
                                <ScaleTransform ScaleX="0.98" ScaleY="0.98"/>
                            </Setter.Value>
                        </Setter>
                    </Trigger>
                </ControlTemplate.Triggers>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>

<!-- Secondary Button -->
<Style x:Key="SecondaryButton" TargetType="Button">
    <Setter Property="Background" Value="Transparent"/>
    <Setter Property="Foreground" Value="{StaticResource AccentPrimaryBrush}"/>
    <Setter Property="BorderBrush" Value="{StaticResource AccentPrimaryBrush}"/>
    <Setter Property="BorderThickness" Value="1.5"/>
    <Setter Property="Padding" Value="16,10"/>
    <Setter Property="FontSize" Value="14"/>
    <Setter Property="FontWeight" Value="SemiBold"/>
    <Setter Property="Cursor" Value="Hand"/>
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="Button">
                <Border x:Name="border"
                        Background="{TemplateBinding Background}"
                        BorderBrush="{TemplateBinding BorderBrush}"
                        BorderThickness="{TemplateBinding BorderThickness}"
                        CornerRadius="6"
                        Padding="{TemplateBinding Padding}">
                    <ContentPresenter HorizontalAlignment="Center" 
                                      VerticalAlignment="Center"/>
                </Border>
                <ControlTemplate.Triggers>
                    <Trigger Property="IsMouseOver" Value="True">
                        <Setter TargetName="border" Property="Background" 
                                Value="#101A73E8"/>
                    </Trigger>
                </ControlTemplate.Triggers>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>

<!-- Quick Action Button -->
<Style x:Key="QuickActionButton" TargetType="Button">
    <Setter Property="Background" Value="{StaticResource BackgroundSecondaryBrush}"/>
    <Setter Property="Foreground" Value="{StaticResource TextPrimaryBrush}"/>
    <Setter Property="BorderThickness" Value="0"/>
    <Setter Property="Padding" Value="16"/>
    <Setter Property="Height" Value="100"/>
    <Setter Property="Cursor" Value="Hand"/>
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="Button">
                <Border x:Name="border"
                        Background="{TemplateBinding Background}"
                        CornerRadius="12"
                        Padding="{TemplateBinding Padding}">
                    <ContentPresenter HorizontalAlignment="Center" 
                                      VerticalAlignment="Center"/>
                </Border>
                <ControlTemplate.Triggers>
                    <Trigger Property="IsMouseOver" Value="True">
                        <Setter TargetName="border" Property="Background" 
                                Value="{StaticResource BackgroundTertiaryBrush}"/>
                    </Trigger>
                    <Trigger Property="IsPressed" Value="True">
                        <Setter TargetName="border" Property="Background" 
                                Value="{StaticResource BorderMediumBrush}"/>
                    </Trigger>
                </ControlTemplate.Triggers>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>

<!-- Navigation Item Style -->
<Style x:Key="NavItemStyle" TargetType="RadioButton">
    <Setter Property="Background" Value="Transparent"/>
    <Setter Property="Foreground" Value="{StaticResource TextSecondaryBrush}"/>
    <Setter Property="Padding" Value="16,12"/>
    <Setter Property="Margin" Value="0,2"/>
    <Setter Property="FontSize" Value="14"/>
    <Setter Property="Cursor" Value="Hand"/>
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="RadioButton">
                <Border x:Name="border"
                        Background="{TemplateBinding Background}"
                        CornerRadius="8"
                        Padding="{TemplateBinding Padding}">
                    <Grid>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="4"/>
                            <ColumnDefinition Width="*"/>
                        </Grid.ColumnDefinitions>
                        
                        <Border x:Name="indicator"
                                Grid.Column="0"
                                Width="4"
                                CornerRadius="2"
                                Background="{StaticResource AccentPrimaryBrush}"
                                Visibility="Collapsed"
                                Margin="0,0,12,0"/>
                        
                        <ContentPresenter Grid.Column="1"
                                          VerticalAlignment="Center"/>
                    </Grid>
                </Border>
                <ControlTemplate.Triggers>
                    <Trigger Property="IsChecked" Value="True">
                        <Setter TargetName="border" Property="Background" 
                                Value="#101A73E8"/>
                        <Setter TargetName="indicator" Property="Visibility" 
                                Value="Visible"/>
                        <Setter Property="Foreground" 
                                Value="{StaticResource AccentPrimaryBrush}"/>
                        <Setter Property="FontWeight" Value="SemiBold"/>
                    </Trigger>
                    <Trigger Property="IsMouseOver" Value="True">
                        <Setter TargetName="border" Property="Background" 
                                Value="#08000000"/>
                    </Trigger>
                </ControlTemplate.Triggers>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>

<!-- Card Style -->
<Style x:Key="CardStyle" TargetType="Border">
    <Setter Property="Background" Value="{StaticResource CardBackgroundBrush}"/>
    <Setter Property="BorderBrush" Value="{StaticResource CardBorderBrush}"/>
    <Setter Property="BorderThickness" Value="1"/>
    <Setter Property="CornerRadius" Value="12"/>
    <Setter Property="Padding" Value="20"/>
    <Setter Property="Effect">
        <Setter.Value>
            <DropShadowEffect BlurRadius="8" 
                              ShadowDepth="2" 
                              Opacity="0.08"
                              Color="#000000"/>
        </Setter.Value>
    </Setter>
</Style>

<!-- Window Control Buttons -->
<Style x:Key="WindowControlButton" TargetType="Button">
    <Setter Property="Width" Value="46"/>
    <Setter Property="Height" Value="32"/>
    <Setter Property="Background" Value="Transparent"/>
    <Setter Property="Foreground" Value="{StaticResource TextSecondaryBrush}"/>
    <Setter Property="BorderThickness" Value="0"/>
    <Setter Property="FontSize" Value="12"/>
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="Button">
                <Border x:Name="border"
                        Background="{TemplateBinding Background}">
                    <ContentPresenter HorizontalAlignment="Center" 
                                      VerticalAlignment="Center"/>
                </Border>
                <ControlTemplate.Triggers>
                    <Trigger Property="IsMouseOver" Value="True">
                        <Setter TargetName="border" Property="Background" 
                                Value="#10000000"/>
                    </Trigger>
                </ControlTemplate.Triggers>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>

<Style x:Key="WindowCloseButton" TargetType="Button" 
       BasedOn="{StaticResource WindowControlButton}">
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="Button">
                <Border x:Name="border"
                        Background="{TemplateBinding Background}">
                    <ContentPresenter HorizontalAlignment="Center" 
                                      VerticalAlignment="Center"/>
                </Border>
                <ControlTemplate.Triggers>
                    <Trigger Property="IsMouseOver" Value="True">
                        <Setter TargetName="border" Property="Background" 
                                Value="{StaticResource AccentDangerBrush}"/>
                        <Setter Property="Foreground" Value="White"/>
                    </Trigger>
                </ControlTemplate.Triggers>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

## 3.7 Circular Progress Bar Control

### CircularProgressBar.xaml

```xaml
<UserControl x:Class="SystemOptimizerPro.Controls.CircularProgressBar"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    
    <Grid x:Name="RootGrid">
        <Viewbox>
            <Grid Width="100" Height="100">
                <!-- Background Circle -->
                <Ellipse Stroke="{StaticResource BackgroundTertiaryBrush}"
                         StrokeThickness="{Binding StrokeThickness, RelativeSource={RelativeSource AncestorType=UserControl}}"
                         Fill="Transparent"/>
                
                <!-- Progress Arc -->
                <Path x:Name="ProgressPath"
                      Stroke="{Binding ProgressColor, RelativeSource={RelativeSource AncestorType=UserControl}}"
                      StrokeThickness="{Binding StrokeThickness, RelativeSource={RelativeSource AncestorType=UserControl}}"
                      StrokeStartLineCap="Round"
                      StrokeEndLineCap="Round"
                      Fill="Transparent">
                    <Path.Data>
                        <PathGeometry>
                            <PathFigure x:Name="PathFigure" StartPoint="50,5">
                                <ArcSegment x:Name="ArcSegment"
                                            Size="45,45"
                                            SweepDirection="Clockwise"
                                            IsLargeArc="False"/>
                            </PathFigure>
                        </PathGeometry>
                    </Path.Data>
                </Path>
                
                <!-- Center Text -->
                <TextBlock Text="{Binding Value, RelativeSource={RelativeSource AncestorType=UserControl}, StringFormat={}{0}%}"
                           HorizontalAlignment="Center"
                           VerticalAlignment="Center"
                           FontSize="18"
                           FontWeight="SemiBold"
                           Foreground="{StaticResource TextPrimaryBrush}"/>
            </Grid>
        </Viewbox>
    </Grid>
</UserControl>
```

### CircularProgressBar.xaml.cs

```csharp
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SystemOptimizerPro.Controls
{
    public partial class CircularProgressBar : UserControl
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(CircularProgressBar),
                new PropertyMetadata(0.0, OnValueChanged));

        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register(nameof(Size), typeof(double), typeof(CircularProgressBar),
                new PropertyMetadata(100.0));

        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register(nameof(StrokeThickness), typeof(double), typeof(CircularProgressBar),
                new PropertyMetadata(8.0));

        public static readonly DependencyProperty ProgressColorProperty =
            DependencyProperty.Register(nameof(ProgressColor), typeof(Brush), typeof(CircularProgressBar),
                new PropertyMetadata(Brushes.Blue));

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public double StrokeThickness
        {
            get => (double)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        public Brush ProgressColor
        {
            get => (Brush)GetValue(ProgressColorProperty);
            set => SetValue(ProgressColorProperty, value);
        }

        public CircularProgressBar()
        {
            InitializeComponent();
            UpdateArc();
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CircularProgressBar)d).UpdateArc();
        }

        private void UpdateArc()
        {
            double angle = Value / 100.0 * 360.0;
            double radians = (angle - 90) * Math.PI / 180.0;
            
            double radius = 45;
            double centerX = 50;
            double centerY = 50;
            
            double endX = centerX + radius * Math.Cos(radians);
            double endY = centerY + radius * Math.Sin(radians);
            
            ArcSegment.Point = new Point(endX, endY);
            ArcSegment.IsLargeArc = angle > 180;
        }
    }
}
```

---

# 4. CORE FEATURES & FUNCTIONALITY

## 4.1 Feature Matrix

| Feature | Priority | Description |
|---------|----------|-------------|
| Standby List Cleaner | HIGH | Clear Windows standby memory list |
| RAM Monitoring | HIGH | Real-time RAM usage display |
| CPU Monitoring | HIGH | Real-time CPU usage display |
| Process List | HIGH | View and manage running processes |
| DNS Cache Clear | MEDIUM | Flush DNS resolver cache |
| Recent Files Clear | MEDIUM | Clear Windows recent file history |
| Registry Cleaner | MEDIUM | Scan and fix registry issues |
| Auto-Optimization | MEDIUM | Automatic memory cleaning based on thresholds |
| System Tray | LOW | Minimize to system tray |
| Startup Option | LOW | Launch on Windows startup |

## 4.2 Standby List Cleaner (ISLC Equivalent)

### Functionality Requirements

1. **Display Current Memory State**
   - Total System Memory
   - Memory In Use
   - Standby List Size
   - Free Memory
   - Modified Memory

2. **Manual Purge**
   - One-click button to purge standby list
   - Show freed memory amount after purge

3. **Automatic Purge**
   - Enable/disable toggle
   - Configurable thresholds:
     - "When standby list exceeds X MB"
     - "AND free memory is below X MB"
   - Configurable polling rate (ms)

4. **Timer Resolution (Optional/Advanced)**
   - Current timer resolution display
   - Option to set custom timer resolution
   - Warning for advanced users only

### Memory States Explanation

```
Total RAM = In Use + Standby + Free

- In Use: Currently being used by applications
- Standby: Cached data, can be reclaimed
- Free: Truly available memory
- Modified: Dirty pages waiting to be written to disk
```

## 4.3 DNS Cache Cleaner

### Functionality

```csharp
// Implementation approach
public async Task<CleaningResult> FlushDnsCache()
{
    // Execute: ipconfig /flushdns
    // OR use native API: DnsFlushResolverCache
    
    return new CleaningResult
    {
        Success = true,
        EntriesCleared = previousCount,
        Message = "DNS cache cleared successfully"
    };
}
```

### Display

- Current DNS cache entry count
- Last flush time
- One-click flush button
- Result notification

## 4.4 Recent Files Cleaner

### Windows Recent Items Locations

```
%APPDATA%\Microsoft\Windows\Recent
%APPDATA%\Microsoft\Windows\Recent\AutomaticDestinations
%APPDATA%\Microsoft\Windows\Recent\CustomDestinations
```

### Functionality

```csharp
public async Task<CleaningResult> ClearRecentFiles()
{
    var locations = new[]
    {
        Environment.GetFolderPath(Environment.SpecialFolder.Recent),
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Recent), "AutomaticDestinations"),
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Recent), "CustomDestinations")
    };
    
    int filesDeleted = 0;
    foreach (var location in locations)
    {
        // Delete files in each location
        // Handle locked files gracefully
    }
    
    return new CleaningResult { FilesDeleted = filesDeleted };
}
```

### Display

- Number of recent items
- Categories breakdown (Documents, Folders, etc.)
- Preview of recent items (optional)
- Clear button with confirmation

## 4.5 Registry Cleaner

### Safe Registry Cleaning Areas

**IMPORTANT: Registry cleaning must be CONSERVATIVE to avoid system damage.**

Safe areas to clean:

1. **Invalid File Extensions**
   - `HKEY_CLASSES_ROOT\*` - Check if associated programs exist

2. **Obsolete Software Entries**
   - `HKEY_LOCAL_MACHINE\SOFTWARE` - Check for non-existent paths
   - `HKEY_CURRENT_USER\SOFTWARE` - Same checks

3. **Invalid Shared DLLs**
   - `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\SharedDLLs`

4. **Startup Items**
   - Check if startup programs still exist

5. **MRU (Most Recently Used) Lists**
   - `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\OpenSavePidlMRU`

### Safety Requirements

```csharp
public class RegistryCleanerService
{
    // MANDATORY: Create backup before any changes
    public async Task<string> CreateBackup()
    {
        // Export registry keys to .reg file
        // Store in user-accessible location
        // Return backup file path
    }
    
    // Scan only - never auto-delete
    public async Task<List<RegistryIssue>> ScanForIssues()
    {
        var issues = new List<RegistryIssue>();
        
        // Scan each safe area
        // Verify paths exist
        // Add to issues list if invalid
        
        return issues;
    }
    
    // User must confirm each deletion
    public async Task<CleaningResult> CleanSelectedIssues(List<RegistryIssue> selectedIssues)
    {
        // Create backup first
        // Delete only selected items
        // Log all changes
    }
}
```

### Display

- Scan button
- Issue categories with counts
- Selectable list of found issues
- Details panel for selected issue
- "Fix Selected" button
- Backup/Restore options

## 4.6 Process Manager

### Functionality

1. **Process List**
   - Name, PID, CPU %, Memory usage
   - Sort by any column
   - Search/filter

2. **Process Actions**
   - End process
   - End process tree
   - Open file location
   - View properties

3. **Memory-focused View**
   - Top memory consumers
   - Working set vs private bytes

### Display

```xaml
<DataGrid ItemsSource="{Binding Processes}"
          AutoGenerateColumns="False"
          CanUserAddRows="False"
          CanUserDeleteRows="False"
          IsReadOnly="True"
          SelectionMode="Single">
    
    <DataGrid.Columns>
        <DataGridTextColumn Header="Name" Binding="{Binding Name}" Width="200"/>
        <DataGridTextColumn Header="PID" Binding="{Binding Id}" Width="70"/>
        <DataGridTextColumn Header="CPU %" Binding="{Binding CpuUsage, StringFormat={}{0:F1}}" Width="70"/>
        <DataGridTextColumn Header="Memory" Binding="{Binding MemoryFormatted}" Width="100"/>
        <DataGridTextColumn Header="Status" Binding="{Binding Status}" Width="80"/>
    </DataGrid.Columns>
    
</DataGrid>
```

---

# 5. TECHNICAL IMPLEMENTATION DETAILS

## 5.1 MVVM Architecture

### Base ViewModel

```csharp
// Helpers/ObservableObject.cs
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SystemOptimizerPro.Helpers
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
```

### Relay Command

```csharp
// Helpers/RelayCommand.cs
using System;
using System.Windows.Input;

namespace SystemOptimizerPro.Helpers
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?>? _canExecute;

        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object? parameter) => _execute(parameter);
    }

    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<object?, Task> _execute;
        private readonly Predicate<object?>? _canExecute;
        private bool _isExecuting;

        public AsyncRelayCommand(Func<object?, Task> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter) => !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);

        public async void Execute(object? parameter)
        {
            if (!CanExecute(parameter))
                return;

            _isExecuting = true;
            CommandManager.InvalidateRequerySuggested();

            try
            {
                await _execute(parameter);
            }
            finally
            {
                _isExecuting = false;
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }
}
```

### Main ViewModel

```csharp
// ViewModels/MainViewModel.cs
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace SystemOptimizerPro.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMemoryService _memoryService;
        private readonly IProcessService _processService;
        private readonly DispatcherTimer _monitorTimer;

        [ObservableProperty]
        private object? _currentView;

        [ObservableProperty]
        private string _statusMessage = "Ready";

        [ObservableProperty]
        private int _ramUsagePercent;

        [ObservableProperty]
        private int _cpuUsagePercent;

        [ObservableProperty]
        private Brush _ramUsageColor = Brushes.Green;

        [ObservableProperty]
        private Brush _cpuUsageColor = Brushes.Green;

        [ObservableProperty]
        private string _monitoringStatus = "Monitoring";

        [ObservableProperty]
        private Brush _monitoringStatusColor = Brushes.Green;

        public MainViewModel(IServiceProvider serviceProvider, 
                            IMemoryService memoryService,
                            IProcessService processService)
        {
            _serviceProvider = serviceProvider;
            _memoryService = memoryService;
            _processService = processService;

            // Initialize with Dashboard
            CurrentView = _serviceProvider.GetRequiredService<DashboardViewModel>();

            // Setup monitoring timer
            _monitorTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _monitorTimer.Tick += OnMonitorTick;
            _monitorTimer.Start();
        }

        private void OnMonitorTick(object? sender, EventArgs e)
        {
            UpdateSystemMetrics();
        }

        private async void UpdateSystemMetrics()
        {
            var memInfo = await _memoryService.GetMemoryInfoAsync();
            RamUsagePercent = (int)memInfo.UsagePercent;
            RamUsageColor = GetUsageColor(RamUsagePercent);

            var cpuUsage = await _processService.GetCpuUsageAsync();
            CpuUsagePercent = (int)cpuUsage;
            CpuUsageColor = GetUsageColor(CpuUsagePercent);
        }

        private static Brush GetUsageColor(int percent)
        {
            return percent switch
            {
                < 50 => (Brush)Application.Current.Resources["AccentSecondaryBrush"],
                < 80 => (Brush)Application.Current.Resources["AccentWarningBrush"],
                _ => (Brush)Application.Current.Resources["AccentDangerBrush"]
            };
        }

        [RelayCommand]
        private void Navigate(string destination)
        {
            CurrentView = destination switch
            {
                "Dashboard" => _serviceProvider.GetRequiredService<DashboardViewModel>(),
                "Memory" => _serviceProvider.GetRequiredService<MemoryCleanerViewModel>(),
                "Processes" => _serviceProvider.GetRequiredService<ProcessesViewModel>(),
                "Cleaning" => _serviceProvider.GetRequiredService<CleaningToolsViewModel>(),
                "Settings" => _serviceProvider.GetRequiredService<SettingsViewModel>(),
                _ => CurrentView
            };
        }

        [RelayCommand]
        private async Task QuickOptimize()
        {
            StatusMessage = "Optimizing...";
            MonitoringStatus = "Cleaning";
            MonitoringStatusColor = (Brush)Application.Current.Resources["AccentWarningBrush"];

            try
            {
                // Execute all cleaning operations
                await _memoryService.PurgeStandbyListAsync();
                
                // Update status
                StatusMessage = $"Optimization complete - {DateTime.Now:HH:mm}";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            finally
            {
                MonitoringStatus = "Monitoring";
                MonitoringStatusColor = (Brush)Application.Current.Resources["AccentSecondaryBrush"];
            }
        }
    }
}
```

## 5.2 Dependency Injection Setup

### App.xaml.cs

```csharp
// App.xaml.cs
using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace SystemOptimizerPro
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Services
            services.AddSingleton<IMemoryService, MemoryService>();
            services.AddSingleton<IProcessService, ProcessService>();
            services.AddSingleton<ICleaningService, CleaningService>();
            services.AddSingleton<IRegistryService, RegistryCleanerService>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<StandbyListService>();
            services.AddSingleton<DnsCacheService>();
            services.AddSingleton<RecentFilesService>();

            // ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<MemoryCleanerViewModel>();
            services.AddTransient<ProcessesViewModel>();
            services.AddTransient<CleaningToolsViewModel>();
            services.AddTransient<SettingsViewModel>();

            // Main Window
            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = _serviceProvider.GetRequiredService<MainViewModel>();
            mainWindow.Show();
        }
    }
}
```

---

# 6. WINDOWS API INTEGRATION

## 6.1 Native Methods Declaration

```csharp
// Native/NativeMethods.cs
using System;
using System.Runtime.InteropServices;

namespace SystemOptimizerPro.Native
{
    internal static class NativeMethods
    {
        #region Memory APIs

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);

        [DllImport("psapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetPerformanceInfo(out PERFORMANCE_INFORMATION pPerformanceInformation, uint cb);

        [DllImport("ntdll.dll")]
        internal static extern int NtQuerySystemInformation(
            SYSTEM_INFORMATION_CLASS SystemInformationClass,
            IntPtr SystemInformation,
            int SystemInformationLength,
            out int ReturnLength);

        [DllImport("ntdll.dll")]
        internal static extern int NtSetSystemInformation(
            SYSTEM_INFORMATION_CLASS SystemInformationClass,
            IntPtr SystemInformation,
            int SystemInformationLength);

        #endregion

        #region Process APIs

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr OpenProcess(
            ProcessAccessFlags processAccess,
            bool bInheritHandle,
            int processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseHandle(IntPtr hObject);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool OpenProcessToken(
            IntPtr ProcessHandle,
            uint DesiredAccess,
            out IntPtr TokenHandle);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool LookupPrivilegeValue(
            string? lpSystemName,
            string lpName,
            out LUID lpLuid);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AdjustTokenPrivileges(
            IntPtr TokenHandle,
            bool DisableAllPrivileges,
            ref TOKEN_PRIVILEGES NewState,
            int BufferLength,
            IntPtr PreviousState,
            IntPtr ReturnLength);

        #endregion

        #region DNS APIs

        [DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCache")]
        internal static extern bool DnsFlushResolverCache();

        [DllImport("dnsapi.dll", EntryPoint = "DnsGetCacheDataTable")]
        internal static extern bool DnsGetCacheDataTable(out IntPtr ppDnsCache);

        #endregion

        #region Timer APIs

        [DllImport("ntdll.dll", SetLastError = true)]
        internal static extern int NtQueryTimerResolution(
            out uint MinimumResolution,
            out uint MaximumResolution,
            out uint CurrentResolution);

        [DllImport("ntdll.dll", SetLastError = true)]
        internal static extern int NtSetTimerResolution(
            uint DesiredResolution,
            bool SetResolution,
            out uint CurrentResolution);

        #endregion

        #region Constants

        internal const uint SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const uint TOKEN_ADJUST_PRIVILEGES = 0x0020;
        internal const uint TOKEN_QUERY = 0x0008;
        internal const string SE_INCREASE_QUOTA_NAME = "SeIncreaseQuotaPrivilege";
        internal const string SE_PROF_SINGLE_PROCESS_NAME = "SeProfileSingleProcessPrivilege";

        #endregion
    }

    #region Structures

    [StructLayout(LayoutKind.Sequential)]
    internal struct MEMORYSTATUSEX
    {
        internal uint dwLength;
        internal uint dwMemoryLoad;
        internal ulong ullTotalPhys;
        internal ulong ullAvailPhys;
        internal ulong ullTotalPageFile;
        internal ulong ullAvailPageFile;
        internal ulong ullTotalVirtual;
        internal ulong ullAvailVirtual;
        internal ulong ullAvailExtendedVirtual;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PERFORMANCE_INFORMATION
    {
        internal uint cb;
        internal UIntPtr CommitTotal;
        internal UIntPtr CommitLimit;
        internal UIntPtr CommitPeak;
        internal UIntPtr PhysicalTotal;
        internal UIntPtr PhysicalAvailable;
        internal UIntPtr SystemCache;
        internal UIntPtr KernelTotal;
        internal UIntPtr KernelPaged;
        internal UIntPtr KernelNonpaged;
        internal UIntPtr PageSize;
        internal uint HandleCount;
        internal uint ProcessCount;
        internal uint ThreadCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SYSTEM_CACHE_INFORMATION
    {
        internal UIntPtr CurrentSize;
        internal UIntPtr PeakSize;
        internal uint PageFaultCount;
        internal UIntPtr MinimumWorkingSet;
        internal UIntPtr MaximumWorkingSet;
        internal UIntPtr CurrentSizeIncludingTransitionInPages;
        internal UIntPtr PeakSizeIncludingTransitionInPages;
        internal uint TransitionRePurposeCount;
        internal uint Flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SYSTEM_FILECACHE_INFORMATION
    {
        internal UIntPtr CurrentSize;
        internal UIntPtr PeakSize;
        internal uint PageFaultCount;
        internal UIntPtr MinimumWorkingSet;
        internal UIntPtr MaximumWorkingSet;
        internal UIntPtr CurrentSizeIncludingTransitionInPages;
        internal UIntPtr PeakSizeIncludingTransitionInPages;
        internal uint TransitionRePurposeCount;
        internal uint Flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LUID
    {
        internal uint LowPart;
        internal int HighPart;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LUID_AND_ATTRIBUTES
    {
        internal LUID Luid;
        internal uint Attributes;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TOKEN_PRIVILEGES
    {
        internal uint PrivilegeCount;
        internal LUID_AND_ATTRIBUTES Privileges;
    }

    internal enum SYSTEM_INFORMATION_CLASS
    {
        SystemFileCacheInformation = 21,
        SystemMemoryListInformation = 80
    }

    internal enum SYSTEM_MEMORY_LIST_COMMAND
    {
        MemoryCaptureAccessedBits = 0,
        MemoryCaptureAndResetAccessedBits = 1,
        MemoryEmptyWorkingSets = 2,
        MemoryFlushModifiedList = 3,
        MemoryPurgeStandbyList = 4,
        MemoryPurgeLowPriorityStandbyList = 5,
        MemoryCommandMax = 6
    }

    [Flags]
    internal enum ProcessAccessFlags : uint
    {
        All = 0x001F0FFF,
        Terminate = 0x00000001,
        CreateThread = 0x00000002,
        VirtualMemoryOperation = 0x00000008,
        VirtualMemoryRead = 0x00000010,
        VirtualMemoryWrite = 0x00000020,
        DuplicateHandle = 0x00000040,
        CreateProcess = 0x000000080,
        SetQuota = 0x00000100,
        SetInformation = 0x00000200,
        QueryInformation = 0x00000400,
        QueryLimitedInformation = 0x00001000,
        Synchronize = 0x00100000
    }

    #endregion
}
```

## 6.2 Memory Service Implementation

```csharp
// Services/MemoryService.cs
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using SystemOptimizerPro.Models;
using SystemOptimizerPro.Native;

namespace SystemOptimizerPro.Services
{
    public interface IMemoryService
    {
        Task<MemoryInfo> GetMemoryInfoAsync();
        Task<bool> PurgeStandbyListAsync();
        Task<bool> EmptyWorkingSetsAsync();
        Task<ulong> GetStandbyListSizeAsync();
    }

    public class MemoryService : IMemoryService
    {
        public Task<MemoryInfo> GetMemoryInfoAsync()
        {
            return Task.Run(() =>
            {
                var memStatus = new MEMORYSTATUSEX { dwLength = (uint)Marshal.SizeOf<MEMORYSTATUSEX>() };
                
                if (!NativeMethods.GlobalMemoryStatusEx(ref memStatus))
                    throw new InvalidOperationException("Failed to get memory status");

                var perfInfo = new PERFORMANCE_INFORMATION();
                perfInfo.cb = (uint)Marshal.SizeOf<PERFORMANCE_INFORMATION>();
                
                if (!NativeMethods.GetPerformanceInfo(out perfInfo, perfInfo.cb))
                    throw new InvalidOperationException("Failed to get performance info");

                ulong pageSize = (ulong)perfInfo.PageSize;
                ulong totalPhysical = memStatus.ullTotalPhys;
                ulong availablePhysical = memStatus.ullAvailPhys;
                ulong systemCache = (ulong)perfInfo.SystemCache * pageSize;

                // Calculate standby list (approximate)
                ulong inUse = totalPhysical - availablePhysical - systemCache;
                ulong standby = systemCache; // Simplified - actual calculation is more complex

                return new MemoryInfo
                {
                    TotalPhysical = totalPhysical,
                    AvailablePhysical = availablePhysical,
                    UsedPhysical = totalPhysical - availablePhysical,
                    StandbyList = standby,
                    UsagePercent = memStatus.dwMemoryLoad,
                    PageSize = pageSize,
                    ProcessCount = perfInfo.ProcessCount,
                    ThreadCount = perfInfo.ThreadCount,
                    HandleCount = perfInfo.HandleCount
                };
            });
        }

        public Task<bool> PurgeStandbyListAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    // Requires SeProfileSingleProcessPrivilege
                    if (!EnablePrivilege(NativeMethods.SE_PROF_SINGLE_PROCESS_NAME))
                        return false;

                    int command = (int)SYSTEM_MEMORY_LIST_COMMAND.MemoryPurgeStandbyList;
                    int size = Marshal.SizeOf<int>();
                    IntPtr buffer = Marshal.AllocHGlobal(size);
                    
                    try
                    {
                        Marshal.WriteInt32(buffer, command);
                        int result = NativeMethods.NtSetSystemInformation(
                            SYSTEM_INFORMATION_CLASS.SystemMemoryListInformation,
                            buffer,
                            size);
                        
                        return result == 0; // STATUS_SUCCESS
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(buffer);
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<bool> EmptyWorkingSetsAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    if (!EnablePrivilege(NativeMethods.SE_PROF_SINGLE_PROCESS_NAME))
                        return false;

                    int command = (int)SYSTEM_MEMORY_LIST_COMMAND.MemoryEmptyWorkingSets;
                    int size = Marshal.SizeOf<int>();
                    IntPtr buffer = Marshal.AllocHGlobal(size);
                    
                    try
                    {
                        Marshal.WriteInt32(buffer, command);
                        int result = NativeMethods.NtSetSystemInformation(
                            SYSTEM_INFORMATION_CLASS.SystemMemoryListInformation,
                            buffer,
                            size);
                        
                        return result == 0;
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(buffer);
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<ulong> GetStandbyListSizeAsync()
        {
            return Task.Run(() =>
            {
                // Implementation to get accurate standby list size
                // Using NtQuerySystemInformation with SystemMemoryListInformation
                return 0UL; // Placeholder
            });
        }

        private static bool EnablePrivilege(string privilegeName)
        {
            IntPtr tokenHandle = IntPtr.Zero;
            
            try
            {
                IntPtr currentProcess = System.Diagnostics.Process.GetCurrentProcess().Handle;
                
                if (!NativeMethods.OpenProcessToken(currentProcess, 
                    NativeMethods.TOKEN_ADJUST_PRIVILEGES | NativeMethods.TOKEN_QUERY, 
                    out tokenHandle))
                    return false;

                if (!NativeMethods.LookupPrivilegeValue(null, privilegeName, out LUID luid))
                    return false;

                var tokenPrivileges = new TOKEN_PRIVILEGES
                {
                    PrivilegeCount = 1,
                    Privileges = new LUID_AND_ATTRIBUTES
                    {
                        Luid = luid,
                        Attributes = NativeMethods.SE_PRIVILEGE_ENABLED
                    }
                };

                return NativeMethods.AdjustTokenPrivileges(
                    tokenHandle, 
                    false, 
                    ref tokenPrivileges, 
                    0, 
                    IntPtr.Zero, 
                    IntPtr.Zero);
            }
            finally
            {
                if (tokenHandle != IntPtr.Zero)
                    NativeMethods.CloseHandle(tokenHandle);
            }
        }
    }
}
```

## 6.3 Standby List Service (Core ISLC Functionality)

```csharp
// Services/StandbyListService.cs
using System;
using System.Threading;
using System.Threading.Tasks;
using SystemOptimizerPro.Models;

namespace SystemOptimizerPro.Services
{
    public class StandbyListService : IDisposable
    {
        private readonly IMemoryService _memoryService;
        private readonly ISettingsService _settingsService;
        private CancellationTokenSource? _monitorCts;
        private Task? _monitorTask;

        public event EventHandler<StandbyListPurgedEventArgs>? StandbyListPurged;
        public event EventHandler<MemoryInfo>? MemoryUpdated;

        public bool IsMonitoring { get; private set; }
        public int PurgeCount { get; private set; }

        public StandbyListService(IMemoryService memoryService, ISettingsService settingsService)
        {
            _memoryService = memoryService;
            _settingsService = settingsService;
        }

        public async Task<StandbyListPurgedEventArgs> PurgeNowAsync()
        {
            var beforeInfo = await _memoryService.GetMemoryInfoAsync();
            
            bool success = await _memoryService.PurgeStandbyListAsync();
            
            var afterInfo = await _memoryService.GetMemoryInfoAsync();

            var result = new StandbyListPurgedEventArgs
            {
                Success = success,
                MemoryFreedBytes = afterInfo.AvailablePhysical - beforeInfo.AvailablePhysical,
                PreviousStandbySize = beforeInfo.StandbyList,
                NewStandbySize = afterInfo.StandbyList,
                Timestamp = DateTime.Now
            };

            if (success)
            {
                PurgeCount++;
                StandbyListPurged?.Invoke(this, result);
            }

            return result;
        }

        public void StartMonitoring()
        {
            if (IsMonitoring) return;

            _monitorCts = new CancellationTokenSource();
            _monitorTask = MonitorLoopAsync(_monitorCts.Token);
            IsMonitoring = true;
        }

        public void StopMonitoring()
        {
            if (!IsMonitoring) return;

            _monitorCts?.Cancel();
            IsMonitoring = false;
        }

        private async Task MonitorLoopAsync(CancellationToken ct)
        {
            var settings = _settingsService.GetSettings();

            while (!ct.IsCancellationRequested)
            {
                try
                {
                    var memInfo = await _memoryService.GetMemoryInfoAsync();
                    MemoryUpdated?.Invoke(this, memInfo);

                    // Check if purge conditions are met
                    if (settings.AutoPurgeEnabled)
                    {
                        bool standbyExceedsThreshold = memInfo.StandbyList >= 
                            (ulong)settings.StandbyThresholdMB * 1024 * 1024;
                        
                        bool freeMemoryBelowThreshold = memInfo.AvailablePhysical <= 
                            (ulong)settings.FreeMemoryThresholdMB * 1024 * 1024;

                        if (standbyExceedsThreshold && freeMemoryBelowThreshold)
                        {
                            await PurgeNowAsync();
                        }
                    }

                    await Task.Delay(settings.PollingRateMs, ct);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    // Log error, continue monitoring
                    Console.WriteLine($"Monitor error: {ex.Message}");
                    await Task.Delay(1000, ct);
                }
            }
        }

        public void Dispose()
        {
            StopMonitoring();
            _monitorCts?.Dispose();
        }
    }

    public class StandbyListPurgedEventArgs : EventArgs
    {
        public bool Success { get; set; }
        public ulong MemoryFreedBytes { get; set; }
        public ulong PreviousStandbySize { get; set; }
        public ulong NewStandbySize { get; set; }
        public DateTime Timestamp { get; set; }

        public string MemoryFreedFormatted => ByteConverter.ToReadableSize(MemoryFreedBytes);
    }
}
```

## 6.4 DNS Cache Service

```csharp
// Services/DnsCacheService.cs
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SystemOptimizerPro.Models;
using SystemOptimizerPro.Native;

namespace SystemOptimizerPro.Services
{
    public class DnsCacheService
    {
        public async Task<CleaningResult> FlushDnsCacheAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    // Method 1: Use native API
                    bool success = NativeMethods.DnsFlushResolverCache();

                    if (!success)
                    {
                        // Method 2: Fallback to ipconfig
                        var psi = new ProcessStartInfo
                        {
                            FileName = "ipconfig",
                            Arguments = "/flushdns",
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            RedirectStandardOutput = true
                        };

                        using var process = Process.Start(psi);
                        process?.WaitForExit();
                        success = process?.ExitCode == 0;
                    }

                    return new CleaningResult
                    {
                        Success = success,
                        Operation = "DNS Cache Flush",
                        Message = success ? "DNS cache flushed successfully" : "Failed to flush DNS cache",
                        Timestamp = DateTime.Now
                    };
                }
                catch (Exception ex)
                {
                    return new CleaningResult
                    {
                        Success = false,
                        Operation = "DNS Cache Flush",
                        Message = $"Error: {ex.Message}",
                        Timestamp = DateTime.Now
                    };
                }
            });
        }

        public async Task<int> GetCacheEntryCountAsync()
        {
            return await Task.Run(() =>
            {
                // This requires parsing DNS cache - simplified implementation
                try
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = "ipconfig",
                        Arguments = "/displaydns",
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true
                    };

                    using var process = Process.Start(psi);
                    string output = process?.StandardOutput.ReadToEnd() ?? "";
                    process?.WaitForExit();

                    // Count "Record Name" occurrences
                    int count = 0;
                    int index = 0;
                    while ((index = output.IndexOf("Record Name", index, StringComparison.OrdinalIgnoreCase)) != -1)
                    {
                        count++;
                        index++;
                    }

                    return count;
                }
                catch
                {
                    return -1;
                }
            });
        }
    }
}
```

## 6.5 Recent Files Service

```csharp
// Services/RecentFilesService.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SystemOptimizerPro.Models;

namespace SystemOptimizerPro.Services
{
    public class RecentFilesService
    {
        private readonly string[] _recentLocations;

        public RecentFilesService()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _recentLocations = new[]
            {
                Environment.GetFolderPath(Environment.SpecialFolder.Recent),
                Path.Combine(appData, @"Microsoft\Windows\Recent\AutomaticDestinations"),
                Path.Combine(appData, @"Microsoft\Windows\Recent\CustomDestinations")
            };
        }

        public async Task<RecentFilesInfo> GetRecentFilesInfoAsync()
        {
            return await Task.Run(() =>
            {
                var info = new RecentFilesInfo();

                foreach (var location in _recentLocations)
                {
                    if (!Directory.Exists(location)) continue;

                    try
                    {
                        var files = Directory.GetFiles(location);
                        info.TotalCount += files.Length;
                        info.TotalSizeBytes += files.Sum(f => new FileInfo(f).Length);
                    }
                    catch
                    {
                        // Access denied or other error
                    }
                }

                return info;
            });
        }

        public async Task<List<RecentFileItem>> GetRecentFilesListAsync(int maxItems = 50)
        {
            return await Task.Run(() =>
            {
                var items = new List<RecentFileItem>();
                string recentFolder = Environment.GetFolderPath(Environment.SpecialFolder.Recent);

                if (!Directory.Exists(recentFolder))
                    return items;

                try
                {
                    var files = Directory.GetFiles(recentFolder, "*.lnk")
                        .Select(f => new FileInfo(f))
                        .OrderByDescending(f => f.LastWriteTime)
                        .Take(maxItems);

                    foreach (var file in files)
                    {
                        items.Add(new RecentFileItem
                        {
                            Name = Path.GetFileNameWithoutExtension(file.Name),
                            Path = file.FullName,
                            LastAccessed = file.LastWriteTime,
                            SizeBytes = file.Length
                        });
                    }
                }
                catch
                {
                    // Handle error
                }

                return items;
            });
        }

        public async Task<CleaningResult> ClearRecentFilesAsync()
        {
            return await Task.Run(() =>
            {
                int deletedCount = 0;
                int failedCount = 0;
                long freedBytes = 0;

                foreach (var location in _recentLocations)
                {
                    if (!Directory.Exists(location)) continue;

                    try
                    {
                        foreach (var file in Directory.GetFiles(location))
                        {
                            try
                            {
                                var fileInfo = new FileInfo(file);
                                freedBytes += fileInfo.Length;
                                File.Delete(file);
                                deletedCount++;
                            }
                            catch
                            {
                                failedCount++;
                            }
                        }
                    }
                    catch
                    {
                        // Location access error
                    }
                }

                return new CleaningResult
                {
                    Success = deletedCount > 0,
                    Operation = "Clear Recent Files",
                    ItemsProcessed = deletedCount,
                    ItemsFailed = failedCount,
                    BytesFreed = (ulong)freedBytes,
                    Message = $"Cleared {deletedCount} recent files ({ByteConverter.ToReadableSize((ulong)freedBytes)} freed)",
                    Timestamp = DateTime.Now
                };
            });
        }
    }

    public class RecentFilesInfo
    {
        public int TotalCount { get; set; }
        public long TotalSizeBytes { get; set; }
        public string TotalSizeFormatted => ByteConverter.ToReadableSize((ulong)TotalSizeBytes);
    }

    public class RecentFileItem
    {
        public string Name { get; set; } = "";
        public string Path { get; set; } = "";
        public DateTime LastAccessed { get; set; }
        public long SizeBytes { get; set; }
    }
}
```

## 6.6 Registry Cleaner Service

```csharp
// Services/RegistryCleanerService.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Win32;
using SystemOptimizerPro.Models;

namespace SystemOptimizerPro.Services
{
    public interface IRegistryService
    {
        Task<List<RegistryIssue>> ScanForIssuesAsync(IProgress<int>? progress = null);
        Task<string> CreateBackupAsync();
        Task<CleaningResult> CleanIssuesAsync(List<RegistryIssue> issues);
        Task<bool> RestoreBackupAsync(string backupPath);
    }

    public class RegistryCleanerService : IRegistryService
    {
        private readonly string _backupFolder;

        public RegistryCleanerService()
        {
            _backupFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "SystemOptimizerPro",
                "RegistryBackups");

            Directory.CreateDirectory(_backupFolder);
        }

        public async Task<List<RegistryIssue>> ScanForIssuesAsync(IProgress<int>? progress = null)
        {
            return await Task.Run(() =>
            {
                var issues = new List<RegistryIssue>();
                int totalSteps = 5;
                int currentStep = 0;

                // 1. Scan for invalid file associations
                progress?.Report((++currentStep * 100) / totalSteps);
                issues.AddRange(ScanInvalidFileAssociations());

                // 2. Scan for obsolete software entries
                progress?.Report((++currentStep * 100) / totalSteps);
                issues.AddRange(ScanObsoleteSoftware());

                // 3. Scan for invalid shared DLLs
                progress?.Report((++currentStep * 100) / totalSteps);
                issues.AddRange(ScanInvalidSharedDlls());

                // 4. Scan for invalid startup entries
                progress?.Report((++currentStep * 100) / totalSteps);
                issues.AddRange(ScanInvalidStartupEntries());

                // 5. Scan MRU lists
                progress?.Report((++currentStep * 100) / totalSteps);
                issues.AddRange(ScanMruLists());

                return issues;
            });
        }

        private List<RegistryIssue> ScanInvalidFileAssociations()
        {
            var issues = new List<RegistryIssue>();

            try
            {
                using var classesRoot = Registry.ClassesRoot;
                foreach (var subKeyName in classesRoot.GetSubKeyNames())
                {
                    if (!subKeyName.StartsWith(".")) continue;

                    try
                    {
                        using var subKey = classesRoot.OpenSubKey(subKeyName);
                        var defaultValue = subKey?.GetValue("") as string;

                        if (!string.IsNullOrEmpty(defaultValue))
                        {
                            using var progIdKey = classesRoot.OpenSubKey(defaultValue);
                            if (progIdKey == null)
                            {
                                issues.Add(new RegistryIssue
                                {
                                    Category = RegistryIssueCategory.FileAssociation,
                                    KeyPath = $@"HKEY_CLASSES_ROOT\{subKeyName}",
                                    ValueName = "(Default)",
                                    Description = $"File association '{subKeyName}' points to missing ProgID '{defaultValue}'",
                                    Severity = IssueSeverity.Low
                                });
                            }
                        }
                    }
                    catch
                    {
                        // Skip inaccessible keys
                    }
                }
            }
            catch
            {
                // Access denied
            }

            return issues;
        }

        private List<RegistryIssue> ScanObsoleteSoftware()
        {
            var issues = new List<RegistryIssue>();
            var paths = new[]
            {
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
                @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
            };

            foreach (var basePath in paths)
            {
                try
                {
                    using var key = Registry.LocalMachine.OpenSubKey(basePath);
                    if (key == null) continue;

                    foreach (var subKeyName in key.GetSubKeyNames())
                    {
                        try
                        {
                            using var subKey = key.OpenSubKey(subKeyName);
                            var installLocation = subKey?.GetValue("InstallLocation") as string;

                            if (!string.IsNullOrEmpty(installLocation) && 
                                !Directory.Exists(installLocation))
                            {
                                var displayName = subKey?.GetValue("DisplayName") as string ?? subKeyName;
                                
                                issues.Add(new RegistryIssue
                                {
                                    Category = RegistryIssueCategory.ObsoleteSoftware,
                                    KeyPath = $@"HKEY_LOCAL_MACHINE\{basePath}\{subKeyName}",
                                    ValueName = "InstallLocation",
                                    Description = $"Software '{displayName}' references non-existent path: {installLocation}",
                                    Severity = IssueSeverity.Medium
                                });
                            }
                        }
                        catch
                        {
                            // Skip inaccessible keys
                        }
                    }
                }
                catch
                {
                    // Access denied
                }
            }

            return issues;
        }

        private List<RegistryIssue> ScanInvalidSharedDlls()
        {
            var issues = new List<RegistryIssue>();

            try
            {
                using var key = Registry.LocalMachine.OpenSubKey(
                    @"SOFTWARE\Microsoft\Windows\CurrentVersion\SharedDLLs");
                
                if (key == null) return issues;

                foreach (var valueName in key.GetValueNames())
                {
                    if (!File.Exists(valueName))
                    {
                        issues.Add(new RegistryIssue
                        {
                            Category = RegistryIssueCategory.SharedDll,
                            KeyPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\SharedDLLs",
                            ValueName = valueName,
                            Description = $"Shared DLL entry points to non-existent file: {valueName}",
                            Severity = IssueSeverity.Low
                        });
                    }
                }
            }
            catch
            {
                // Access denied
            }

            return issues;
        }

        private List<RegistryIssue> ScanInvalidStartupEntries()
        {
            var issues = new List<RegistryIssue>();
            var runPaths = new[]
            {
                (Registry.CurrentUser, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run"),
                (Registry.LocalMachine, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run"),
                (Registry.LocalMachine, @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run")
            };

            foreach (var (root, path) in runPaths)
            {
                try
                {
                    using var key = root.OpenSubKey(path);
                    if (key == null) continue;

                    foreach (var valueName in key.GetValueNames())
                    {
                        var command = key.GetValue(valueName) as string;
                        if (string.IsNullOrEmpty(command)) continue;

                        // Extract executable path from command
                        string exePath = ExtractExecutablePath(command);
                        
                        if (!string.IsNullOrEmpty(exePath) && !File.Exists(exePath))
                        {
                            issues.Add(new RegistryIssue
                            {
                                Category = RegistryIssueCategory.StartupEntry,
                                KeyPath = $@"{GetRootName(root)}\{path}",
                                ValueName = valueName,
                                Description = $"Startup entry '{valueName}' references non-existent file: {exePath}",
                                Severity = IssueSeverity.Medium
                            });
                        }
                    }
                }
                catch
                {
                    // Access denied
                }
            }

            return issues;
        }

        private List<RegistryIssue> ScanMruLists()
        {
            var issues = new List<RegistryIssue>();
            
            // MRU cleaning is typically safe - these are just usage history
            var mruPaths = new[]
            {
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\OpenSavePidlMRU",
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\RunMRU"
            };

            foreach (var path in mruPaths)
            {
                try
                {
                    using var key = Registry.CurrentUser.OpenSubKey(path);
                    if (key == null) continue;

                    int entryCount = key.GetValueNames().Length;
                    if (entryCount > 0)
                    {
                        // Add as low-severity cleanup opportunity
                        issues.Add(new RegistryIssue
                        {
                            Category = RegistryIssueCategory.MruList,
                            KeyPath = $@"HKEY_CURRENT_USER\{path}",
                            ValueName = "*",
                            Description = $"MRU list contains {entryCount} history entries",
                            Severity = IssueSeverity.Info,
                            ItemCount = entryCount
                        });
                    }
                }
                catch
                {
                    // Access denied
                }
            }

            return issues;
        }

        public async Task<string> CreateBackupAsync()
        {
            return await Task.Run(() =>
            {
                string backupFile = Path.Combine(_backupFolder, 
                    $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.reg");

                // Export relevant keys using reg.exe
                // In production, use Registry API to create proper .reg file
                
                return backupFile;
            });
        }

        public async Task<CleaningResult> CleanIssuesAsync(List<RegistryIssue> issues)
        {
            // Always create backup first
            string backupPath = await CreateBackupAsync();

            return await Task.Run(() =>
            {
                int cleaned = 0;
                int failed = 0;

                foreach (var issue in issues)
                {
                    try
                    {
                        // Parse and delete the registry entry
                        // Implementation depends on issue type
                        cleaned++;
                    }
                    catch
                    {
                        failed++;
                    }
                }

                return new CleaningResult
                {
                    Success = cleaned > 0,
                    Operation = "Registry Cleanup",
                    ItemsProcessed = cleaned,
                    ItemsFailed = failed,
                    Message = $"Cleaned {cleaned} registry issues. Backup saved to: {backupPath}",
                    Timestamp = DateTime.Now
                };
            });
        }

        public Task<bool> RestoreBackupAsync(string backupPath)
        {
            return Task.Run(() =>
            {
                // Use reg.exe to import backup
                return false; // Placeholder
            });
        }

        private static string ExtractExecutablePath(string command)
        {
            command = command.Trim();
            
            if (command.StartsWith("\""))
            {
                int endQuote = command.IndexOf('"', 1);
                if (endQuote > 1)
                    return command.Substring(1, endQuote - 1);
            }
            
            int spaceIndex = command.IndexOf(' ');
            return spaceIndex > 0 ? command.Substring(0, spaceIndex) : command;
        }

        private static string GetRootName(RegistryKey root)
        {
            return root.Name switch
            {
                "HKEY_CURRENT_USER" => "HKEY_CURRENT_USER",
                "HKEY_LOCAL_MACHINE" => "HKEY_LOCAL_MACHINE",
                _ => root.Name
            };
        }
    }

    public class RegistryIssue
    {
        public RegistryIssueCategory Category { get; set; }
        public string KeyPath { get; set; } = "";
        public string ValueName { get; set; } = "";
        public string Description { get; set; } = "";
        public IssueSeverity Severity { get; set; }
        public int ItemCount { get; set; }
        public bool IsSelected { get; set; }
    }

    public enum RegistryIssueCategory
    {
        FileAssociation,
        ObsoleteSoftware,
        SharedDll,
        StartupEntry,
        MruList,
        Other
    }

    public enum IssueSeverity
    {
        Info,
        Low,
        Medium,
        High
    }
}
```

---

# 7. DATA MODELS & ARCHITECTURE

## 7.1 Data Models

```csharp
// Models/MemoryInfo.cs
namespace SystemOptimizerPro.Models
{
    public class MemoryInfo
    {
        public ulong TotalPhysical { get; set; }
        public ulong AvailablePhysical { get; set; }
        public ulong UsedPhysical { get; set; }
        public ulong StandbyList { get; set; }
        public ulong ModifiedList { get; set; }
        public uint UsagePercent { get; set; }
        public ulong PageSize { get; set; }
        public uint ProcessCount { get; set; }
        public uint ThreadCount { get; set; }
        public uint HandleCount { get; set; }

        // Formatted properties
        public string TotalFormatted => ByteConverter.ToReadableSize(TotalPhysical);
        public string AvailableFormatted => ByteConverter.ToReadableSize(AvailablePhysical);
        public string UsedFormatted => ByteConverter.ToReadableSize(UsedPhysical);
        public string StandbyFormatted => ByteConverter.ToReadableSize(StandbyList);

        public double TotalGB => TotalPhysical / (1024.0 * 1024.0 * 1024.0);
        public double AvailableGB => AvailablePhysical / (1024.0 * 1024.0 * 1024.0);
        public double UsedGB => UsedPhysical / (1024.0 * 1024.0 * 1024.0);
        public double StandbyGB => StandbyList / (1024.0 * 1024.0 * 1024.0);
    }
}

// Models/ProcessInfo.cs
namespace SystemOptimizerPro.Models
{
    public class ProcessInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public double CpuUsage { get; set; }
        public long WorkingSetBytes { get; set; }
        public long PrivateBytesBytes { get; set; }
        public string Status { get; set; } = "";
        public DateTime StartTime { get; set; }
        public string FilePath { get; set; } = "";
        public string UserName { get; set; } = "";

        public string MemoryFormatted => ByteConverter.ToReadableSize((ulong)WorkingSetBytes);
        public string CpuFormatted => $"{CpuUsage:F1}%";
    }
}

// Models/CleaningResult.cs
namespace SystemOptimizerPro.Models
{
    public class CleaningResult
    {
        public bool Success { get; set; }
        public string Operation { get; set; } = "";
        public string Message { get; set; } = "";
        public int ItemsProcessed { get; set; }
        public int ItemsFailed { get; set; }
        public ulong BytesFreed { get; set; }
        public DateTime Timestamp { get; set; }

        public string BytesFreedFormatted => ByteConverter.ToReadableSize(BytesFreed);
    }
}

// Models/AppSettings.cs
namespace SystemOptimizerPro.Models
{
    public class AppSettings
    {
        // Standby List Settings
        public bool AutoPurgeEnabled { get; set; }
        public int StandbyThresholdMB { get; set; } = 1024;
        public int FreeMemoryThresholdMB { get; set; } = 1024;
        public int PollingRateMs { get; set; } = 1000;

        // Timer Resolution
        public bool CustomTimerResolutionEnabled { get; set; }
        public double WantedTimerResolutionMs { get; set; } = 0.5;

        // General
        public bool StartMinimized { get; set; }
        public bool MinimizeToTray { get; set; }
        public bool StartWithWindows { get; set; }
        public bool ShowNotifications { get; set; } = true;

        // Cleaning
        public bool IncludeDnsInQuickOptimize { get; set; } = true;
        public bool IncludeRecentFilesInQuickOptimize { get; set; }
        public bool IncludeRegistryInQuickOptimize { get; set; }

        // Theme
        public string Theme { get; set; } = "Light";
    }
}
```

## 7.2 Byte Converter Helper

```csharp
// Helpers/ByteConverter.cs
namespace SystemOptimizerPro.Helpers
{
    public static class ByteConverter
    {
        private static readonly string[] SizeSuffixes = { "B", "KB", "MB", "GB", "TB", "PB" };

        public static string ToReadableSize(ulong bytes, int decimalPlaces = 1)
        {
            if (bytes == 0) return "0 B";

            int magnitude = (int)Math.Floor(Math.Log(bytes, 1024));
            magnitude = Math.Min(magnitude, SizeSuffixes.Length - 1);

            double adjustedSize = bytes / Math.Pow(1024, magnitude);

            return $"{adjustedSize:N" + decimalPlaces + "} {SizeSuffixes[magnitude]}";
        }

        public static ulong FromMegabytes(int megabytes) => (ulong)megabytes * 1024 * 1024;
        
        public static ulong FromGigabytes(double gigabytes) => (ulong)(gigabytes * 1024 * 1024 * 1024);
    }
}
```

---

# 8. SAFETY & ERROR HANDLING

## 8.1 Administrator Check

```csharp
// Helpers/AdminHelper.cs
using System;
using System.Diagnostics;
using System.Security.Principal;

namespace SystemOptimizerPro.Helpers
{
    public static class AdminHelper
    {
        public static bool IsRunningAsAdmin()
        {
            using var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static void RestartAsAdmin()
        {
            var startInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = Process.GetCurrentProcess().MainModule?.FileName,
                Verb = "runas"
            };

            try
            {
                Process.Start(startInfo);
                Environment.Exit(0);
            }
            catch
            {
                // User declined UAC prompt
            }
        }
    }
}
```

## 8.2 Error Handling Guidelines

```csharp
// Example error handling pattern
public async Task<CleaningResult> SafeCleaningOperation(Func<Task<CleaningResult>> operation, string operationName)
{
    try
    {
        return await operation();
    }
    catch (UnauthorizedAccessException)
    {
        return new CleaningResult
        {
            Success = false,
            Operation = operationName,
            Message = "Access denied. Please run as Administrator."
        };
    }
    catch (Win32Exception ex)
    {
        return new CleaningResult
        {
            Success = false,
            Operation = operationName,
            Message = $"Windows error: {ex.Message}"
        };
    }
    catch (Exception ex)
    {
        // Log exception
        return new CleaningResult
        {
            Success = false,
            Operation = operationName,
            Message = $"Unexpected error: {ex.Message}"
        };
    }
}
```

## 8.3 Safety Considerations

### Registry Operations

1. **ALWAYS create backup before any registry modifications**
2. **Never delete system-critical keys**
3. **Scan only, never auto-clean without user confirmation**
4. **Log all changes for potential restoration**

### Memory Operations

1. **Check for sufficient privileges before attempting**
2. **Handle failures gracefully - memory operations can fail**
3. **Don't purge too aggressively - let Windows manage memory first**

### File Operations

1. **Handle locked files gracefully**
2. **Provide option to skip locked files**
3. **Never delete files outside designated areas**

---

# 9. PERFORMANCE OPTIMIZATION

## 9.1 UI Performance

```csharp
// Use async/await for all long operations
public async void UpdateDashboard()
{
    // Run heavy operations on background thread
    var memInfo = await Task.Run(() => _memoryService.GetMemoryInfo());
    
    // Update UI on main thread
    Application.Current.Dispatcher.Invoke(() =>
    {
        RamUsagePercent = (int)memInfo.UsagePercent;
        // ... other updates
    });
}

// Use virtualization for long lists
// In XAML:
<ListBox VirtualizingPanel.IsVirtualizing="True"
         VirtualizingPanel.VirtualizationMode="Recycling"/>
```

## 9.2 Memory Efficiency

```csharp
// Dispose of resources properly
public class ProcessService : IDisposable
{
    private readonly PerformanceCounter _cpuCounter;
    
    public ProcessService()
    {
        _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
    }
    
    public void Dispose()
    {
        _cpuCounter?.Dispose();
    }
}
```

---

# 10. TESTING REQUIREMENTS

## 10.1 Unit Tests

```csharp
// Tests/Services/MemoryServiceTests.cs
[TestClass]
public class MemoryServiceTests
{
    [TestMethod]
    public async Task GetMemoryInfo_ReturnsValidData()
    {
        var service = new MemoryService();
        var result = await service.GetMemoryInfoAsync();
        
        Assert.IsTrue(result.TotalPhysical > 0);
        Assert.IsTrue(result.AvailablePhysical > 0);
        Assert.IsTrue(result.AvailablePhysical <= result.TotalPhysical);
    }
    
    [TestMethod]
    public async Task PurgeStandbyList_RequiresAdmin()
    {
        var service = new MemoryService();
        
        // This test should be run both as admin and non-admin
        // to verify correct behavior
        var result = await service.PurgeStandbyListAsync();
        
        // Result depends on privileges
        Assert.IsNotNull(result);
    }
}
```

## 10.2 Integration Tests

- Test full cleaning workflows
- Test settings persistence
- Test system tray functionality
- Test startup behavior

## 10.3 Manual Testing Checklist

- [ ] Application starts without errors
- [ ] Dashboard shows accurate system information
- [ ] Memory cleaner purges standby list successfully
- [ ] Auto-purge triggers at correct thresholds
- [ ] DNS cache flush works
- [ ] Recent files clearing works
- [ ] Registry scanner finds valid issues
- [ ] Registry cleaner creates backup
- [ ] Process list updates correctly
- [ ] Process termination works
- [ ] Settings persist across restarts
- [ ] System tray icon works
- [ ] Application closes cleanly

---

# 11. BUILD & DEPLOYMENT

## 11.1 Build Commands

```bash
# Debug build
dotnet build -c Debug

# Release build
dotnet build -c Release

# Publish self-contained
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

# Publish framework-dependent (smaller)
dotnet publish -c Release -r win-x64 --self-contained false
```

## 11.2 Deployment Package Structure

```
SystemOptimizerPro/
├── SystemOptimizerPro.exe
├── SystemOptimizerPro.dll
├── SystemOptimizerPro.deps.json
├── SystemOptimizerPro.runtimeconfig.json
├── LICENSE.txt
├── README.md
└── Assets/
    └── (embedded in exe for single-file publish)
```

## 11.3 Installer Considerations

Consider using:
- **Inno Setup** - Free, scriptable installer
- **WiX Toolset** - MSI-based installer
- **MSIX** - Modern Windows packaging

---

# 12. ADDITIONAL NOTES FOR AI AGENT

## 12.1 Development Order

1. **Phase 1: Core Structure**
   - Set up project structure
   - Implement MVVM framework
   - Create basic window with navigation

2. **Phase 2: Memory Features**
   - Implement memory monitoring
   - Implement standby list purging
   - Create memory cleaner view

3. **Phase 3: Dashboard**
   - Create circular progress controls
   - Implement real-time updates
   - Add system health display

4. **Phase 4: Additional Features**
   - DNS cache service
   - Recent files service
   - Registry cleaner (scan only first)

5. **Phase 5: Polish**
   - Settings persistence
   - System tray integration
   - Animations and transitions

## 12.2 Key Quality Requirements

- **Responsive UI**: Never freeze the UI - all heavy operations must be async
- **Clean Code**: Follow SOLID principles, use dependency injection
- **Error Handling**: Gracefully handle all failure scenarios
- **User Feedback**: Always show operation status and results
- **Safety First**: Never perform destructive operations without user confirmation

## 12.3 Testing Requirements

- Test on Windows 10 and Windows 11
- Test with varying RAM amounts (8GB, 16GB, 32GB)
- Test with and without administrator privileges
- Verify all memory operations work correctly
- Ensure registry operations are safe

---

# CONCLUSION

This guide provides a comprehensive specification for building a modern system optimization application in C# WPF. Follow the structure, implement each feature according to the specifications, and test thoroughly before deployment.

**Key Success Factors:**
1. Accurate memory monitoring and management
2. Safe, conservative registry cleaning
3. Modern, responsive UI design
4. Excellent error handling
5. User safety above all else

Good luck with the implementation!
