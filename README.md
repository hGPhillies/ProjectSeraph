# Project Seraph – Digital registrering af vitale værdier

Project Seraph er et **3-tier system** til digital registrering af vitale værdier.

## Systemarkitektur

- **Webklient:** React + TypeScript (SPA)
- **Admin-klient:** C# WPF (desktop)
- **Backend / Middleware:** REST API + WebSocket
- **Database:** MongoDB

Middleware og MongoDB køres via **Docker Compose**.

---

## Krav

Følgende skal være installeret for at kunne køre projektet lokalt:

- Docker + Docker Compose
- Node.js + npm
- .NET SDK + Visual Studio (til admin-klienten i WPF)

---

## Kør projektet lokalt

### 1) Start backend + MongoDB (Docker)

Gå til backend-/docker-mappen: ProjectSeraphBackend

Kør derefter:

docker compose up --build
Backend og MongoDB starter nu op og kan kommunikere via Docker Compose-netværket.

2) Start webklienten (React)
Gå til webklient-mappen: projectseraph.client
Kør følgende kommandoer:
npm install
npm run dev
Åbn derefter den URL, som Vite/React dev-serveren viser i terminalen.

Du kan nu:

registrere en måling

sende målingen til sygeplejersken via systemet

3) Start admin-klienten (WPF)
Åbn admin-projektet i Visual Studio: ProjectSeraph_AdminClient
Kør projektet (Start / F5)

Log ind med følgende testbruger:

Brugernavn: 1111110000

Adgangskode: admin

Brug admin-flowet i applikationen (inkl. knappen til admin-funktionalitet)

Admin-klienten er en WPF desktop-applikation bygget med MVVM og kommunikerer direkte med backend.

API / Dokumentation
Backend stiller Swagger-dokumentation til rådighed for alle API-endpoints, når backend kører
