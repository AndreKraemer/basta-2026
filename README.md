# .NET MAUI 10: Der Rebellen-Spirit ist zurück

Demo-Code zum gleichnamigen Vortrag von **André Krämer** (Microsoft MVP) auf der
[BASTA! Herbst 2026](https://basta.net/net-framework-c/dotnet-maui-native-app-entwicklung/) in Mainz.

> Nach .NET MAUI 6 war das größte Feature jedes Releases meist „weniger kritische Bugs“.
> Seit dem Release von .NET MAUI 10 hat das MAUI-Team – angefeuert durch KI-gestützte
> Entwicklung – in Rekordzeit nachgelegt: XAML Source Generation, globale und implizite
> XML-Namespaces, C#-Expressions in XAML, Material 3, HybridWebView-Verbesserungen,
> neues Tooling und experimentelle Plattformen. Vieles davon ist bereits über die
> .NET MAUI 10 Service Releases verfügbar – nicht erst mit .NET 11.

---

## 📦 Die Beispielprojekte

Jedes Projekt ist eine eigenständige Solution (`.slnx`) und lässt sich unabhängig öffnen.
Vorbereitete Demos liegen unter `prepared/`, die auf der Bühne live bearbeitete Demo unter `live/`.

### XAML: Performance & weniger Tipparbeit

| Projekt | Beschreibung |
| --- | --- |
| [`MauiAppDemoNet10`](prepared/MauiAppDemoNet10/) | MAUI-App mit aktivierter XAML **Source Generation** (`<MauiXamlInflator>SourceGen</MauiXamlInflator>`). |
| [`MauiAppDemoNet10SourceGenDebug`](prepared/MauiAppDemoNet10SourceGenDebug/) | Dieselbe App **mit** Source Generation – Vergleichsbasis für das Debugging-Verhalten. |
| [`MauiAppDemoNet10NoSourceGenDebug`](prepared/MauiAppDemoNet10NoSourceGenDebug/) | Dieselbe App **ohne** Source Generation (Runtime-Inflation im Debug). |
| [`MauiAppDemoNet10NoSourceGenRelease`](prepared/MauiAppDemoNet10NoSourceGenRelease/) | Vergleichsprojekt ohne Source Generation für den Release-Pfad. |
| [`MauiAppDemoNet10Xaml`](prepared/MauiAppDemoNet10Xaml/) | **Globale und implizite XML-Namespaces** sowie **C#-Expressions in XAML** (Preview, benötigt `EnablePreviewFeatures`). |

> 💡 Der Effekt von Source Generation lässt sich am besten zeigen, indem man dieselbe
> App mit und ohne aktivierten Inflator baut und debuggt.

### Plattformen & Design

| Projekt | Beschreibung |
| --- | --- |
| [`MauiAppMaterial3Demo`](prepared/MauiAppMaterial3Demo/) | **Material 3** auf Android per `<UseMaterial3>true</UseMaterial3>` – verfügbar seit .NET MAUI 10 SR6. |

### Web-Oberflächen in nativen Apps

| Projekt | Beschreibung |
| --- | --- |
| [`BlazorHybridDemo`](prepared/BlazorHybridDemo/) | Blazor Hybrid (MAUI + Web): Über `WebResourceRequested` fängt die App den Request auf `users.json` ab und liefert eine eigene Sprecherliste – ohne Änderung an der Web-Oberfläche. |
| [`MiracleListVue`](prepared/MiracleListVue/) | Bestehende Vue.js-SPA (MiracleList von Dr. Holger Schwichtenberg), ergänzt um eine HybridWebView-Brücke (`useHybridWebView.ts`). Läuft weiterhin im Browser. |
| [`MiracleListVueHybridDemo`](prepared/MiracleListVueHybridDemo/) | Die Vue-SPA als native MAUI-App per **HybridWebView**: JavaScript ↔ .NET in beide Richtungen, natives Teilen, Word-Export (OpenXML) und Request-Interception. |

### Künstliche Intelligenz

| Projekt | Beschreibung |
| --- | --- |
| [`DontLetMeExpire`](prepared/DontLetMeExpire/) | Lebensmittel-Verwaltung gegen Verschwendung: Barcode-Scan per Kamera (`Camera.MAUI.ZXing` + OpenFoodFacts) und **KI-gestützte Rezeptvorschläge** für ablaufende Lebensmittel über `Microsoft.Extensions.AI` – wahlweise mit **Azure OpenAI** oder **Apple Intelligence** als `IChatClient`. |

### Live-Demo

| Projekt | Beschreibung |
| --- | --- |
| [`MauiAppDemoLive`](live/MauiAppDemoLive/) | Ausgangsstand für die Live-Demo auf **.NET 11 RC** (XAML-Features, Tooling). |

Im Ordner `.claude/skills` liegen außerdem die **MAUI DevFlow**-Skills, mit denen ein
KI-Agent (z. B. Claude Code) eine laufende MAUI-App inspizieren und steuern kann.

---

## 🛠️ Voraussetzungen

- **.NET 10 SDK** (10.0.401 oder neuer) für alle Demos unter `prepared/`
- **.NET 11 RC SDK** für die Live-Demo unter `live/`
- **.NET MAUI Workload** für beide SDKs
  ```bash
  dotnet workload install maui
  ```
- **Visual Studio 2026** (mit installiertem **.NET MAUI Workload**) –
  optional **VS Code** mit der **.NET MAUI Extension**
- Für iOS/macOS-Targets: ein **Mac** mit aktueller Xcode-Version
- Für `MiracleListVue`: **Node.js** (inkl. npm)
- Optional: die **MAUI CLI**
  ```bash
  dotnet tool install -g Microsoft.Maui.Cli --prerelease
  ```

---

## 🚀 Erste Schritte

### MAUI-App bauen & starten

```bash
cd prepared/MauiAppDemoNet10/MauiAppDemoNet10
dotnet build -t:Run -f net10.0-android        # Android
# oder -f net10.0-windows10.0.19041.0 / net10.0-ios / net10.0-maccatalyst
```

In Visual Studio: Solution öffnen, Zielplattform wählen und mit **F5** starten.

> ⚠️ Mit SDK 10.0.401 schlägt `dotnet run -c Release` für Android beim Deployment fehl
> (Fehler `MSB4221` in `ComputeManagedAssemblies`). `dotnet build -t:Run -c Release -f net10.0-android`
> funktioniert dagegen.

### Vue-SPA für die HybridWebView bauen

```bash
cd prepared/MiracleListVue
npm install
npm run dev              # SPA im Browser
npm run build:webview    # Build für die Einbettung in die HybridWebView
```

Das Ergebnis des WebView-Builds gehört nach
`prepared/MiracleListVueHybridDemo/MiracleListVueHybridDemo/Resources/Raw/wwwroot`.

---

## 🔑 Konfiguration

### MiracleList-Backend (`MiracleListVue` / `MiracleListVueHybridDemo`)

Die SPA nutzt das öffentliche MiracleList-Backend (`https://miraclelistbackend.azurewebsites.net`)
und benötigt daher eine Internetverbindung sowie eine eigene **Client-ID**, die unter
https://miraclelistbackend.azurewebsites.net/clientid beantragt werden kann. Client-ID und
Backend-URL werden in der Datei `.env` hinterlegt.

### KI-Demo (`DontLetMeExpire`)

Die App liest die Zugangsdaten für Azure AI Foundry / Azure OpenAI aus
Umgebungsvariablen – es werden **keine Schlüssel im Repository abgelegt**:

| Variable | Beschreibung |
| --- | --- |
| `AI_Foundry_Endpoint` | Endpoint deiner Azure-AI-/OpenAI-Ressource |
| `AI_Foundry_ApiKey` | Zugehöriger API-Key |

Das verwendete Modell ist im Code auf `gpt-4o` gesetzt. Auf Apple-Plattformen kann
alternativ **Apple Intelligence** als lokaler `IChatClient` genutzt werden.

> ⚠️ Umgebungsvariablen des Entwicklungsrechners stehen nur Apps zur Verfügung, die auf
> diesem Rechner laufen (z. B. Windows). Im Android-Emulator sind sie nicht vorhanden.

> ⚠️ Niemals echte API-Keys committen. Setze die Werte als Umgebungsvariablen oder
> über User Secrets.

---

## 👤 Über den Sprecher

**André Krämer** – Microsoft MVP, Trainer und Berater rund um .NET, .NET MAUI und KI.
🔗 [andrekraemer.de](https://andrekraemer.de) · [github.com/andrekraemer](https://github.com/andrekraemer)

---

## 📄 Lizenz

Die Beispiele dienen Demonstrationszwecken im Rahmen der BASTA! und dürfen frei zum
Lernen verwendet werden. `MiracleListVue` basiert auf dem Beispielprojekt von
Dr. Holger Schwichtenberg ([www.IT-Visions.de](https://www.IT-Visions.de)).
