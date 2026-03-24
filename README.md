# Test Task - Combat Prototype (Unity, Mobile)

A top-down, performance-focused combat system prototype built with Unity.

## Overview

This project demonstrates a lightweight and optimized combat system designed to run on low-spec mobile devices while providing stable performance under heavy load

## Features

- Top-down player movement with a mobile joystick
- Enemy spawn system with configurable limits
- Auto-combat system:
- Bullets 
- Grenades
- Weighted random target selection (based on distance)
- Enemy reactions:
- Flash on impact
- Knockback effect
- Separation behavior
- Runtime debug panel for real-time gameplay tuning
- Graphics integration for FPS and performance monitoring

## Architecture

The project uses a lightweight architecture focused on performance and simplicity:

- MonoBehavior-based game systems
- Object pool for enemies, bullets, and grenades
- Manual movement and collision checking
- Distance-based logic instead of physics queries
- VContainer is used for dependency injection
- Runtime settings system for real-time configuration

## Performance

The prototype is optimized for low-end Android devices:

- No physics simulation (no Rigidbody/Collider)
- Minimal runtime memory allocations thanks to object pooling
- Lightweight graphics
- Optimized update loops
- Stable performance with over 100 active enemies

## Controls

- Movement: On-screen joystick
- Shooting: Automatic

## Debugging Tools

The runtime debug panel allows you to adjust gameplay parameters in real time:

- Enemy spawn rate
- Maximum number of enemies
- Fire rate
- Grenade fire rate
- Number of projectiles per shot

Graphy is used for real-time FPS and memory monitoring.

## Build

- Unity version: 6.3 (URP)
- Target platform: Android
## Notes

The project prioritizes clarity, performance, and maintainability over unnecessary architectural complexity.
