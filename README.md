# 📄 Docs Metadata Editor (Resume ATS Optimizer)

[![.NET](https://img.shields.io/badge/.NET-10.0-blueviolet.svg)](https://dotnet.microsoft.com/download/dotnet/10.0)
[![Avalonia](https://img.shields.io/badge/AvaloniaUI-12.0-blue.svg)](https://avaloniaui.net/)
[![Platform](https://img.shields.io/badge/platform-Windows%20%7C%20Linux%20%7C%20macOS-lightgrey.svg)](#)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](#)

A modern, high-performance cross-platform desktop application built with **Avalonia UI** and **.NET 10** designed to inspect, edit, and optimize PDF resume/CV metadata. Tailor your resume for Applicant Tracking Systems (ATS) by infusing job-specific metadata properties, helping your applications rank higher.

---

## ✨ Key Features

- **🎯 ATS Keyword Optimization Auto-Fill**: Over 24 preconfigured industry-standard developer profiles spanning Frontend, Backend, and Fullstack domains across multiple seniority levels (Intern, Junior, Pleno, Senior, Tech Lead).
- **🔤 Accent & Diacritics Sanitization**: Automatically filters and converts Portuguese characters to their base forms (e.g. `á` to `a`, `ç` to `c`) in ATS templates to prevent indexing failures in legacy parsers.
- **🛠 Comprehensive Metadata Management**: Interactively view and edit essential PDF properties scanned by modern recruitment software:
  - **Title**: Target Job Title
  - **Author**: Candidate Name
  - **Subject**: Core Expertise / Executive Summary
  - **Keywords**: Comma-separated hard skills and tech tags
  - **Comments**: Paragraph-based professional profile summary
- **💾 Flexible Saving Modes**: Overwrite the original resume in-place or download/export an optimized copy.
- **🔔 Native Toasts & Feedback**: Sleek overlay notifications (Success, Information, Error) leveraging Avalonia's `WindowNotificationManager`.
- **🎨 Premium Dark Theme UI**: A refined dark-fluent design featuring responsive grid systems, styled inputs, and attention-grabbing call-to-actions.

---

## 🛠 Tech Stack

- **Framework**: [Avalonia UI v12.0.4](https://github.com/AvaloniaUI/Avalonia) (fluent dark theme)
- **Runtime**: .NET 10 (C# 14)
- **Design Pattern**: MVVM with [CommunityToolkit.Mvvm v8.4.1](https://github.com/CommunityToolkit/MVVM-Samples)
- **PDF Manipulation**: [PdfSharp v6.2.4](https://github.com/empira/PDFsharp)
- **Rendering Engine**: SkiaSharp (bundled with native binaries)

---

## 🚀 Getting Started

### Prerequisites

Ensure you have the following installed:
1. [.NET 10 SDK](https://dotnet.microsoft.com/download)
2. (Optional) [Docker Desktop](https://www.docker.com/products/docker-desktop/) if you want to run containerized multi-platform builds.

### Run Locally

1. **Clone the repository:**
   ```bash
   git clone https://github.com/IgorJFS/Avalonia-Metadata-Editor.git
   cd Avalonia-Metadata-Editor
   ```

2. **Restore dependencies & Run:**
   ```bash
   dotnet restore
   dotnet run
   ```

---

## 📦 Building and Packaging Releases

Because Avalonia applications rely on **SkiaSharp** for rendering, compiling the application as a strict single-file binary can cause runtime `DllNotFoundException` errors on target machines due to unextracted native libraries (`libSkiaSharp.dll`, etc.).

To resolve this, release binaries should be distributed in **compressed folder packages (ZIP/TAR)** containing both the main executable and its native DLLs side-by-side.

### 🐳 Cross-Platform Build with Docker

This repository includes a `Dockerfile` and a PowerShell build script to compile releases for **Windows (x64)**, **Linux (x64)**, and **macOS (x64)** within a controlled container environment.

To build all platforms automatically:
1. Open PowerShell.
2. Execute the release script:
   ```powershell
   .\build-releases.ps1
   ```
3. The packaged folders and binaries will be generated inside the `./releases` directory.

---

## 📂 Project Structure

```
├── Assets/                 # Resources, application icons, and templates
│   └── profiles.json       # 24 preconfigured English and Portuguese ATS profiles
├── Models/                 # Data contracts (ResumeMetadata, AtsProfile)
├── ViewModels/             # MVVM ViewModel layer (MainWindowViewModel logic)
├── Views/                  # UI design and layout (MainWindow XAML markup and code-behind)
├── Services/               # IMetadataService (PdfSharp operations), INotificationService, etc.
├── Docs_Metadata_Editor.csproj
├── Dockerfile              # Multi-stage release container configuration
├── build-releases.ps1      # Local artifact extractor script
└── README.md
```

---

## 🤝 Contributing

Contributions are welcome! Feel free to open issues or submit pull requests for new templates, language translations, or feature improvements.

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.
