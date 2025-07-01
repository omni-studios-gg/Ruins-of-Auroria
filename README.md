# OmniMMO Server Architecture

## Overview

OmniMMO is a scalable, seamless-world MMORPG server built with Unity and FishNet networking. It features a **single FishNet server instance** per world (or shard) that manages an entire seamless game world composed of additively loaded zone subscenes.

The backend services such as authentication, chat, NPC management, and auction house are split into dedicated microservices, allowing for modular scaling and simplified network management.

---

## Architecture

### Single FishNet Server per World

- One FishNet server instance runs the whole seamless world.
- Loads a **main_world** scene which acts as the container.
- Zone environments (e.g., forests, deserts, cities) are loaded/unloaded additively as SubScenes within the same server process.
- This approach allows smooth streaming and seamless player transition between zones without server switches.
- All players share a unified network session managed by the single FishNet server instance.

### Backend Microservices

- **AuthServer** — Handles player authentication and account management.
- **ZoneManager** — Spawns and manages NPCs, events, and world state.
- **ChatServer** — Dedicated real-time chat and messaging service.
- **AuctionHouseService** — Handles player trading, auction listings, and economy.
- Other auxiliary services can be added independently to avoid overloading the FishNet server.

---

## Scalability & Performance

- Designed for **thousands of concurrent players per world** (target: 3,000 - 5,000+ CCU).
- High-end dedicated server hardware with multiple CPU cores, fast RAM, and high bandwidth is recommended.
- Optimizations include:
  - Efficient network message handling via FishNet.
  - Additive SubScene streaming to limit active simulation load.
  - Offloading non-core responsibilities to microservices.
  - Using GPU Instancer and WorldStreamer for client-side performance.

- For player counts beyond 5,000 per world, consider horizontally scaling by deploying multiple shards/worlds.

---

## Tech Stack

| Component              | Technology                           |
|------------------------|------------------------------------|
| Game Server            | Unity (Headless) + FishNet         |
| Networking             | FishNet Networking Framework       |
| Zone & Scene Streaming | Unity Additive Scenes / SubScenes  |
| Backend Services       | Microservices (e.g., Node.js, Python, etc.) |
| Database               | [Your choice, e.g., MySQL, MongoDB] |
| Client-side Optimization | GPU Instancer, WorldStreamer       |

---

## Getting Started

### Prerequisites

- Unity 2023.x or newer
- FishNet Networking (latest stable)
- Backend microservices setup (see respective repos)
- High-end server hardware recommended for deployment

### Running the Server

1. Build the Unity headless server with the **main_world** scene.
2. Configure zone SubScenes to load additively at runtime.
3. Start backend services (AuthServer, ChatServer, ZoneManager, AuctionHouseService).
4. Run the FishNet server instance.
5. Connect clients to the server endpoint to enter the seamless world.

---

## Folder Structure

