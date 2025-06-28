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

-- Dumping structure for table logdb.combat_logs
CREATE TABLE IF NOT EXISTS `combat_logs` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `character_id` int unsigned NOT NULL,
  `target_id` int unsigned NOT NULL,
  `zone_id` smallint unsigned NOT NULL,
  `position_x` float DEFAULT NULL,
  `position_y` float DEFAULT NULL,
  `ability_used` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `ability_id` int unsigned DEFAULT NULL,
  `damage_done` int NOT NULL,
  `critical_hit` tinyint(1) DEFAULT '0',
  `killing_blow` tinyint(1) DEFAULT '0',
  `timestamp` datetime(6) DEFAULT CURRENT_TIMESTAMP(6),
  PRIMARY KEY (`id`),
  KEY `idx_combat_attacker` (`character_id`),
  KEY `idx_combat_target` (`target_id`),
  KEY `idx_combat_timestamp` (`timestamp`),
  KEY `idx_combat_zone` (`zone_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci ROW_FORMAT=COMPRESSED;

-- Data exporting was unselected.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
