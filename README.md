![image](https://github.com/user-attachments/assets/3610bb55-9c2e-47b1-bf31-3108b9303d88)

# ADRES - Gestión Integral para el registro de requerimientos de adquisiciones

[![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen)](http://adres-adolfo.somee.com/)

## Descripción General

**ADRES** es una solución desarrollada en .NET 9 que implementa los principios de **Clean Architecture** para la capa backend y un proyecto web basado en **Razor Pages** para la parte frontend. El sistema expone una serie de APIs RESTful que son consumidas por la aplicación web, permitiendo gestionar de manera integral la información de bienes, adquisiciones, proveedores y unidades organizativas en entornos gubernamentales.

---

## Características Principales

- **Arquitectura limpia:** Separación de capas para mantener la escalabilidad y mantenibilidad del código.
- **Consumo de APIs:** El proyecto web (Razor) consume los servicios expuestos por el backend.
- **Gestión integral:** Funcionalidades para la administración de bienes, proveedores, adquisiciones, y unidades.
- **Interfaz moderna y responsiva:** Desarrollo con Razor Pages que facilita la interacción y el acceso a la información.

---

## Tecnologías Utilizadas

- **Backend:**
  - .NET 9
  - Clean Architecture
  - APIs RESTful

- **Frontend:**
  - ASP.NET Core Razor Pages
  - HTML5, CSS3 y JavaScript

- **Herramientas y Entorno de Desarrollo:**
  - Visual Studio 2022
  - Git para el control de versiones

---

## Estructura del Proyecto

La solución se compone de varios proyectos organizados de la siguiente forma:

- **Core:** Contiene las entidades, interfaces y lógica de negocio.
- **Application:** Implementa los casos de uso y la lógica de aplicación.
- **Infrastructure:** Proporciona implementaciones de acceso a datos, servicios externos y otras dependencias.
- **API:** Proyecto que expone los endpoints RESTful para el consumo de datos.
- **Web:** Proyecto basado en Razor Pages que actúa como frontend, consumiendo las APIs del backend.

> *(La imagen adjunta en el repositorio muestra la estructura completa de la solución.)*

---

## Instalación y Ejecución

### Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Visual Studio 2022 o IDE de tu preferencia compatible con .NET 6
- Git (para clonar el repositorio)

### Clonación del Repositorio

```bash
git clone https://github.com/tu-usuario/adres.git
cd adres
