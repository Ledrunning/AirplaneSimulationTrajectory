# Airplane Simulation Trajectory

## Overview

[![Workflow Status](https://github.com/Ledrunning/AirplaneSimulationTrajectory/actions/workflows/main.yml/badge.svg)](https://github.com/Ledrunning/AirplaneSimulationTrajectory/actions/workflows/main.yml)

**Airplane Simulation Trajectory** is a WPF (Windows Presentation Foundation) 3D application that demonstrates the simulation of an airplane's trajectory over the ground. The application utilizes the [Helix Toolkit](https://github.com/helix-toolkit) (Helix3D) for rendering 3D graphics, providing an immersive experience of tracking the path of an airplane.

## UI layout

 ![](flight.gif)

 ![](aircraft2.gif)

## Features

- **3D Visualization:** Utilizes Helix3D to create a visually engaging representation of an airplane's trajectory.
- **Simulation:** Simulates the movement of an airplane over a given ground, showcasing realistic trajectory dynamics.
- **MVVM Architecture:** Built on the Model-View-ViewModel (MVVM) design pattern, promoting separation of concerns and maintainability.

## Requirements

- **.NET Framework:** This application is built on .NET Framework 4.8. Ensure that the appropriate runtime is installed on the target machine.

## Getting Started

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/yourusername/AirplaneSimulationTrajectory.git

2. **Open the Solution:**
Open the solution file (AirplaneSimulationTrajectory.sln) in Visual Studio.

3. **Configuring.** All necessary application settings are in the configuration.xml file.  
```
<?xml version="1.0" encoding="utf-8" ?>
<settings>
  <timerSpeedMs>100</timerSpeedMs>
  <cloudsOpacity>0.5</cloudsOpacity>
  <routeCoordinates>
    <startPointLat>45.046</startPointLat>
    <startPointLon>7.728</startPointLon>
    <endPointLat>68.608</endPointLat>
    <endPointLon>140.061</endPointLon>
  </routeCoordinates>

  <tubeConfiguration>
    <diameter>0.02</diameter>
    <thetaDiv>10</thetaDiv>
    <opacity>1.0</opacity>
    <color>#94f2d3</color>
  </tubeConfiguration>
  <flightInformation>
    <flightLength>5400</flightLength>
    <totalFlightTime>15</totalFlightTime>
  </flightInformation>
</settings>
```

7. **Build and Run:**
Build the solution and run the application to start the Airplane Simulation Trajectory experience.

## Usage
- Upon launching the application, you will be presented with a 3D visualization of the airplane trajectory.
- Use the provided controls to interact with the simulation, adjust settings, and explore the features.

## Contributing
Contributions are welcome! If you find any issues or have ideas for improvements, please submit them through the issue tracker.

## License
This project is licensed under the MIT License.

## Acknowledgments
Helix Toolkit: A special thanks to the Helix Toolkit community for providing a powerful 3D library for WPF.
