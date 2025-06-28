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

-- Dumping structure for table worlddb.quests
CREATE TABLE IF NOT EXISTS `quests` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `description` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `brief` text COLLATE utf8mb4_unicode_ci,
  `required_level` tinyint unsigned DEFAULT '1',
  `recommended_level` tinyint unsigned DEFAULT NULL,
  `quest_chain_id` int unsigned DEFAULT NULL,
  `chain_position` tinyint unsigned DEFAULT NULL,
  `min_reputation` int DEFAULT '0',
  `reputation_faction_id` smallint unsigned DEFAULT NULL,
  `is_repeatable` tinyint(1) DEFAULT '0',
  `repeat_delay` int unsigned DEFAULT NULL,
  `reward_experience` int unsigned DEFAULT '0',
  `reward_money` int unsigned DEFAULT '0',
  `start_npc_id` int unsigned DEFAULT NULL,
  `end_npc_id` int unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idx_quest_level` (`required_level`),
  KEY `idx_quest_chain` (`quest_chain_id`),
  KEY `idx_quest_start_npc` (`start_npc_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Data exporting was unselected.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
