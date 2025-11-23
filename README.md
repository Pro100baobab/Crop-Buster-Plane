# Crop-Duster-Plane
Physics-based aircraft simulator with map input system control

# Cornfield Flyer - Physical Aircraft Simulator

The project is a physically accurate simulator of the "Cornfield Flyer" aircraft, built on a modular component system using Unity New Input System.

## 🎯 Project Goals

1. **Assemble a complete aircraft** from components (aerodynamics + engine + HUD)
2. **Use New Input System with Action Map**
3. **Display primary telemetry** through UI system

## 🛠 Technical Features

### Physical Model
- Realistic aerodynamics with lift force
- Angle of attack and flight speed calculations
- Stabilization and automatic turn coordination
- Rigidbody-based physics with proper forces and moments

### Control System
- **New Input System** with "AirCraft" action map
- Smooth pitch, roll, and yaw control
- Engine thrust regulation

### Visual Components
- HUD with primary telemetry
- Artificial horizon
- Smooth follow camera

## 🎮 Controls

### Primary Controls
- **Pitch**: `Up/Down Arrow` - raise/lower nose
- **Roll**: `Left/Right Arrow` - tilt wings
- **Yaw**: `A/D` - turn left/right
- **Throttle**: `W/S` - increase/decrease engine power

### Displayed Parameters
- **SPD** - flight speed
- **ALT** - altitude above sea level
- **THR** - current engine thrust (%)
- **Artificial Horizon** - aircraft roll indicator

## 📁 Project Structure

### Main Scripts

#### 1. AircraftController.cs
Main aircraft controller responsible for:
- Flight physics and aerodynamics
- Engine and thrust management
- Input data processing
- Lift force and stabilization calculations

**Key Methods:**
- `HandleEngine()` - engine control
- `HandleAerodynamics()` - aerodynamic calculations
- `ApplyControlTorque()` - control moment application
- `HandleBankedTurn()` - automatic banked turns

#### 2. AircraftHUDController.cs
Pilot interface controller:
- Speed, altitude, and thrust display
- Artificial horizon visualization
- Real-time telemetry updates

#### 3. CameraFollow.cs
Follow camera system:
- Smooth aircraft tracking
- Position and rotation smoothing
- Automatic target following

#### 4. AircraftControls.cs
Generated New Input System map:
- "AirCraft" Action Map with 4 main actions
- Key bindings for complete control

## ⚙️ Physical Parameters

### AircraftController Settings:
- **Pitch Torque**: 12000f (pitch sensitivity)
- **Roll Torque**: 10000f (roll sensitivity)
- **Yaw Torque**: 6000f (yaw sensitivity)
- **Max Thrust**: 100000f (maximum thrust)
- **Lift Factor**: 1.2f (lift coefficient)

## 🚀 Setup and Build

1. Import the project into Unity (version 2021.3+ recommended)
2. Ensure Input System Package is installed
3. Launch the scene and use controls for flight

## 🐛 Known Features

- Aircraft tends to auto-stabilize
- Turns are automatically coordinated during banking
- Sufficient speed and lift required for takeoff
- Gradual throttle changes recommended for smooth control

## 📄 License

Project developed for demonstration of physical modeling in Unity.

---

# Кукурузник - Физический симулятор самолета

Проект представляет собой физически точный симулятор самолета "Кукурузник", построенный на модульной системе компонентов с использованием Unity New Input System.

## 🎯 Цели проекта

1. **Собрать цельный самолёт** из компонентов (аэродинамика + двигатель + HUD)
2. **Использовать New Input System с Action Map**
3. **Показать основную телеметрию** через UI систему

## 🛠 Технические особенности

### Физическая модель
- Реалистичная аэродинамика с подъемной силой
- Учет угла атаки и скорости полета
- Стабилизация и автоматическая координация поворотов
- Физика на основе Rigidbody с правильными силами и моментами

### Система управления
- **New Input System** с картой действий "AirCraft"
- Плавное управление тангажом, креном и рысканием
- Регулировка тяги двигателя

### Визуальная часть
- HUD с основной телеметрией
- Искусственный горизонт
- Следящая камера с плавным перемещением

## 🎮 Управление

### Основное управление
- **Тангаж (Pitch)**: `Стрелка Вверх/Вниз` - поднять/опустить нос
- **Крен (Roll)**: `Стрелка Влево/Вправо` - наклонить крылья
- **Рыскание (Yaw)**: `A/D` - поворот влево/вправо
- **Тяга (Throttle)**: `W/S` - увеличить/уменьшить мощность двигателя

### Отображаемые параметры
- **SPD** - скорость полета
- **ALT** - высота над уровнем моря  
- **THR** - текущая тяга двигателя (%)
- **Искусственный горизонт** - отображение крена самолета

## 📁 Структура проекта

### Основные скрипты

#### 1. AircraftController.cs
Главный контроллер самолета, отвечающий за:
- Физику полета и аэродинамику
- Управление двигателем и тягой
- Обработку входных данных
- Расчет подъемной силы и стабилизации

**Ключевые методы:**
- `HandleEngine()` - управление двигателем
- `HandleAerodynamics()` - аэродинамические расчеты
- `ApplyControlTorque()` - применение управляющих моментов
- `HandleBankedTurn()` - автоматические повороты при крене

#### 2. AircraftHUDController.cs
Контроллер интерфейса пилота:
- Отображение скорости, высоты и тяги
- Визуализация искусственного горизонта
- Обновление телеметрии в реальном времени

#### 3. CameraFollow.cs
Система следящей камеры:
- Плавное сопровождение самолета
- Сглаживание позиции и вращения
- Автоматическое наведение на цель

#### 4. AircraftControls.cs
Сгенерированная карта ввода New Input System:
- Action Map "AirCraft" с 4 основными действиями
- Привязки клавиш для полного управления

## ⚙️ Физические параметры

### Настройки AircraftController:
- **Pitch Torque**: 12000f (чувствительность тангажа)
- **Roll Torque**: 10000f (чувствительность крена) 
- **Yaw Torque**: 6000f (чувствительность рыскания)
- **Max Thrust**: 100000f (максимальная тяга)
- **Lift Factor**: 1.2f (коэффициент подъемной силы)

## 🚀 Запуск и сборка

1. Импортируйте проект в Unity (версия 2021.3+ рекомендуется)
2. Убедитесь, что установлен Input System Package
3. Запустите сцену и используйте управление для полета

## 🐛 Известные особенности

- Самолет имеет тенденцию к автоматической стабилизации
- Повороты координируются автоматически при крене
- Для взлета требуется достаточная скорость и подъемная сила
- Рекомендуется постепенное изменение тяги для плавного управления

## 📄 Лицензия

Проект разработан для демонстрации физического моделирования в Unity.
