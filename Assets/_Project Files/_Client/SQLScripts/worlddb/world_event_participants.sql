-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               8.0.41 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Version:             12.10.0.7034
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

-- Dumping structure for table worlddb.world_event_participants
CREATE TABLE IF NOT EXISTS `world_event_participants` (
  `event_id` int unsigned NOT NULL,
  `character_id` int NOT NULL,
  `join_time` datetime DEFAULT CURRENT_TIMESTAMP,
  `leave_time` datetime DEFAULT NULL,
  `contribution_score` int unsigned DEFAULT '0',
  `rewards_claimed` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`event_id`,`character_id`),
  KEY `idx_event_participation` (`character_id`),
  CONSTRAINT `world_event_participants_ibfk_1` FOREIGN KEY (`event_id`) REFERENCES `world_events` (`id`) ON DELETE CASCADE,
  CONSTRAINT `world_event_participants_ibfk_2` FOREIGN KEY (`character_id`) REFERENCES `characterdb`.`characters` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Data exporting was unselected.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
