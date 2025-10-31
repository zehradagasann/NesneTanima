# [NesneTanıma]

[![Dil](https://img.shields.io/badge/Dil-C%23-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Kütüphane](https://img.shields.io/badge/Kütüphane-OpenCV-green.svg)](https://opencv.org/)
[![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey.svg)]()

[Proje Adınız], C# dili ve OpenCV kütüphanesi kullanılarak Visual Studio'da geliştirilmiş bir [projenin kısa tanımı, örn: gerçek zamanlı yüz tanıma, nesne takibi, görüntü filtreleme] uygulamasıdır.

## 📜 Hakkında

Bu proje, [ 'gerçek zamanlı video akışında yüzleri tespit etmek ve tanımak amacıyla geliştirilmiştir']
## ✨ Özellikler

*   Gerçek zamanlı kamera görüntüsü işleme
*   Görüntüden/Videodan yüz tespiti
*   [Özellik 1,Yüz Tanıma]
*   [Özellik 2 ,Renkli Nesne Tanıma ]
*   [Özellik 3 ,Kenar Tespiti]


## 📸 Ekran Görüntüleri

*Uygulamanızın nasıl göründüğünü göstermek için buraya ekran görüntüleri veya GIF'ler ekleyin.*

![Uygulama Ekran Görüntüsü](<img width="1523" height="769" alt="image" src="https://github.com/user-attachments/assets/165fb9a2-ad28-4033-b026-1775f003bbbe" />)



![Özellik Ekran Görüntüsü](![WhatsApp Görsel 2025-10-31 saat 22 33 03_853166bc](https://github.com/user-attachments/assets/63682916-8e52-4b18-91ba-3d828ca3f746)
)


## 🛠️ Kullanılan Teknolojiler

*   **C#** - (.NET Framework / .NET Core [kullandığınız sürümü belirtin])
*   **Visual Studio 2022** - [veya kullandığınız sürüm]
*   **OpenCvSharp** - [veya Emgu CV gibi kullandığınız OpenCV wrapper kütüphanesi]
*   **[Kullandığınız diğer kütüphaneler, örn: WinForms, WPF, etc.]**

## 🚀 Kurulum

Projeyi yerel makinenizde çalıştırmak için aşağıdaki adımları izleyin.

### Gereksinimler

*   [Visual Studio 2019](https://visualstudio.microsoft.com/) veya daha yeni bir sürüm
*   [.NET Framework 4.7.2](https://dotnet.microsoft.com/download) veya [.NET 6](https://dotnet.microsoft.com/download) (projenize uygun olanı seçin)

### Adımlar

1.  **Repository'yi klonlayın:**
    ```sh
    git clone https://github.com/zehradagasann/NesneTanima.git
    ```
2.  **Projeyi Visual Studio'da açın:**
    Klonladığınız klasördeki `.sln` uzantılı dosyayı Visual Studio ile açın.

3.  **NuGet Paketlerini Yükleyin:**
    Solution Explorer'da projeye sağ tıklayıp "Manage NuGet Packages" seçeneğini seçin. Gerekli paketlerin (özellikle OpenCvSharp) geri yüklenmesini (Restore) sağlayın. Eğer otomatik olarak yüklenmezse, aşağıdaki paketleri kurun:
   
    ```
  

4.  **Projeyi Derleyin (Build):**
    Visual Studio menüsünden `Build > Build Solution` seçeneğini seçin.

5.  **Uygulamayı Çalıştırın:**
    `Start` butonuna basarak veya `F5` tuşuyla uygulamayı başlatın.

## 📖 Kullanım

Kamerayı başlatıp yüzleri ve nesneleri tespit edebilirsiniz.

### Kod Örneği

Projedeki temel bir görüntü işleme fonksiyonunun basit bir örneği:

 private void InitializeOpenCV()
 {
     try
     {
         string exePath = Application.StartupPath;
         string face1 = System.IO.Path.Combine(exePath, "haarcascade_frontalface_default.xml");
         string eye1 = System.IO.Path.Combine(exePath, "haarcascade_eye.xml");

         faceCascade = new CascadeClassifier(face1);
         eyeCascade = new CascadeClassifier(eye1);

         frame = new Mat();

         if (faceCascade.Empty())
         {
             MessageBox.Show($"UYARI: haarcascade_frontalface_default.xml dosyası bulunamadı!\nAranılan konum: {face1}\n\nLütfen dosyayı buraya kopyalayın.");
         }
     }
     catch (Exception ex)
     {
         MessageBox.Show($"OpenCV başlatma hatası: {ex.Message}");
     }
 }

```

## 🤝 Katkıda Bulunma

Katkılarınız projeyi daha iyi bir hale getirecektir! Katkıda bulunmak isterseniz, lütfen aşağıdaki adımları izleyin:

1.  Projeyi Fork'layın.
2.  Yeni bir Feature Branch oluşturun (`git checkout -b feature/AmazingFeature`).
3.  Değişikliklerinizi Commit'leyin (`git commit -m 'Add some AmazingFeature'`).
4.  Branch'inizi Push'layın (`git push origin feature/AmazingFeature`).
5.  Bir Pull Request açın.

Proje Sahibi
Ad Soyad:Zehra Dağaşan
Github:

