# 🌍 Seamless MMO World Server Architecture

This repository documents the architecture and implementation strategy for building a **seamless open-world MMORPG server** using **Unity + FishNet + WorldStreamer + GPU Instancer**.

---

## 📌 Key Features

- ✅ One massive, seamless world per server instance (like ArcheAge)
- ✅ Unity additive scene loading for world zones
- ✅ Only one FishNet server per world (no handoffs)
- ✅ Microservices for scalable features (auth, chat, auction, AI)
- ✅ Client-side streaming with WorldStreamer and GPU Instancer
- ✅ Interest management to reduce network load

---

## 🏗️ Architecture Overview

### Main World
- The main Unity scene (`main_world`) acts as the **parent container**.
- Contains base logic and bootstraps additive loading of zones.

### Additive Zones (SubScenes)
- Each major region (e.g., Dewstone, Solis Headlands) is a **Unity additive scene**.
- Dynamically streamed in/out via `WorldStreamer`.
- GPU Instancer handles visual optimization for high draw calls.

### FishNet Server
- One FishNet instance per world/shard.
- All players in the same session.
- No server switching or zone-handoffs.

### Microservices
| Service         | Purpose                              |
|----------------|----------------------------------------|
| AuthService     | Login, account validation             |
| ChatService     | Channels, whispers, guild chat        |
| AuctionService  | Market listings, bidding, delivery    |
| ZoneManager     | Controls NPC spawns, events per zone  |
| InstanceService | (Optional) For instanced content      |

---

## 📶 Network Strategy

- All subscenes/zones share one network session.
- Interest management via FishNet observers or proximity-based logic.
- No loading screens — world is fully connected.

---

## 🧠 Server Process Flow

1. Launch FishNet server instance
2. Load `main_world`
3. Dynamically stream in zones based on player positions
4. FishNet handles all player sync
5. Microservices handle external systems

---

## 🧪 Performance & Scaling

- Targeting **3k–5k CCU per world instance**
- Optimized via:
  - Additive scene loading
  - Interest management
  - Separate system microservices
  - Hardware: 16–32 core CPU, 128–256GB RAM

---

## 🗂️ Project Stack

- **Unity** (2022+)
- **FishNet** (Networking)
- **WorldStreamer** (Terrain streaming)
- **GPU Instancer** (Rendering optimization)
- **FastAPI/Node.js** (Microservices)
- **Redis/MySQL** (Databases)

---

## 📎 Comparison to ArcheAge

| Feature                  | ArcheAge                | This Project                     |
|--------------------------|-------------------------|----------------------------------|
| Seamless open world     | ✅ Yes                  | ✅ Yes (additive scenes)         |
| Multiple zones           | ✅ Yes                  | ✅ Yes (SubScenes)               |
| Streaming terrain/objects| ✅ Yes (CryEngine)      | ✅ WorldStreamer + GPU Instancer |
| One server per shard     | ✅ Yes                  | ✅ One FishNet instance          |
| Microservice architecture| ✅ Yes                  | ✅ FastAPI/Node-based            |

---

## 🔄 Future Goals

- 🔁 Cross-server PvP (shard instances)
- 🧪 Dynamic zone stress testing
- 🔒 Secure gRPC/gateway load balancers
- 🧭 Admin tools and world control panel

---

## 📫 Contribution

This is a long-term MMORPG backend infrastructure project. Contributions to networking, ECS optimization, world loading, and simulation threading are welcome.

---

## 🧙‍♂️ Maintained by
**Omni Studios GG** – For more info, DM `@MoonSight` or open an issue.

---

## License
MIT

---

> "Built to scale worlds, not just instances."

---

