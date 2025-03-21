CREATE DATABASE  IF NOT EXISTS `sabpat702` /*!40100 DEFAULT CHARACTER SET utf8mb3 COLLATE utf8mb3_hungarian_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `sabpat702`;
-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: mysql    Database: sabpat702
-- ------------------------------------------------------
-- Server version	8.0.41-0ubuntu0.24.04.1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `ai`
--

DROP TABLE IF EXISTS `ai`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ai` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(40) NOT NULL,
  `Description` text NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ai`
--

LOCK TABLES `ai` WRITE;
/*!40000 ALTER TABLE `ai` DISABLE KEYS */;
INSERT INTO `ai` VALUES (1,'Basic','The most basic behaviour'),(2,'Defense','Defensive Behaviour');
/*!40000 ALTER TABLE `ai` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `armor`
--

DROP TABLE IF EXISTS `armor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `armor` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `DEF` int NOT NULL,
  `MDEF` int NOT NULL,
  `SpecialEffect` varchar(300) NOT NULL,
  `Type` int NOT NULL,
  `Price` int NOT NULL,
  `Unique` tinyint NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `armor`
--

LOCK TABLES `armor` WRITE;
/*!40000 ALTER TABLE `armor` DISABLE KEYS */;
INSERT INTO `armor` VALUES (1,'None','No armor just clothes, skin and body hair.',0,0,'None',5,0,0),(2,'Worn Leather Chestpiece','A worn set of leather that is used for protection.',1,0,'None',2,0,0),(3,'Old Travel Boots','A pair of old boots made for walking in the countryside that has seen better days.',1,0,'None',4,0,0),(4,'Dirty Cloak','A long cloak that was dragged through everything a journey can find.',1,0,'None',2,0,0),(5,'Mana Touched Rag','A rag like cloak with a faint sense of magic within.',0,1,'None',2,0,0),(6,'Worn Leather Knee Pads','An old pair of leather knee pads.',1,0,'None',3,0,0),(7,'Rusting Chainmail','An old piece of weak and rusting armor.',2,0,'None',2,0,0);
/*!40000 ALTER TABLE `armor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `buff_and_debuff`
--

DROP TABLE IF EXISTS `buff_and_debuff`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `buff_and_debuff` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `Affect` varchar(500) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `buff_and_debuff`
--

LOCK TABLES `buff_and_debuff` WRITE;
/*!40000 ALTER TABLE `buff_and_debuff` DISABLE KEYS */;
INSERT INTO `buff_and_debuff` VALUES (1,'Marked','The mark of death increases damage received by a fix amount based on the level of the inflicter.','Damage Calculation'),(2,'Burning','Currently on fire and will receive damage at the end of the turn based on level.','Turn End'),(3,'Shielded (Physical)','Shielded from all sources of physical damage.','Turn End'),(4,'Taunted','Can only target a taunting enemy.','Targeting'),(5,'Taunting','Taunted enemies can only target you.','Targeting');
/*!40000 ALTER TABLE `buff_and_debuff` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `category`
--

DROP TABLE IF EXISTS `category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `category` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Category` varchar(30) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Category_UNIQUE` (`Category`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `category`
--

LOCK TABLES `category` WRITE;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
/*!40000 ALTER TABLE `category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `comment`
--

DROP TABLE IF EXISTS `comment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `comment` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Content` text NOT NULL,
  `Date` date NOT NULL,
  `CommenterId` int NOT NULL,
  `PostId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `user_comment_idx` (`CommenterId`),
  KEY `post_comment_idx` (`PostId`),
  CONSTRAINT `post_comment` FOREIGN KEY (`PostId`) REFERENCES `post` (`Id`),
  CONSTRAINT `user_comment` FOREIGN KEY (`CommenterId`) REFERENCES `user` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `comment`
--

LOCK TABLES `comment` WRITE;
/*!40000 ALTER TABLE `comment` DISABLE KEYS */;
/*!40000 ALTER TABLE `comment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `consumable`
--

DROP TABLE IF EXISTS `consumable`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `consumable` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `SpecialEffect` varchar(200) NOT NULL,
  `Price` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `consumable`
--

LOCK TABLES `consumable` WRITE;
/*!40000 ALTER TABLE `consumable` DISABLE KEYS */;
INSERT INTO `consumable` VALUES (1,'Test Item','Item for testin purpose','None',10);
/*!40000 ALTER TABLE `consumable` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dungeon`
--

DROP TABLE IF EXISTS `dungeon`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dungeon` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `Length` int NOT NULL,
  `Gold` int NOT NULL,
  `Experience` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dungeon`
--

LOCK TABLES `dungeon` WRITE;
/*!40000 ALTER TABLE `dungeon` DISABLE KEYS */;
INSERT INTO `dungeon` VALUES (1,'Test Place','A place for testing',10,10,10);
/*!40000 ALTER TABLE `dungeon` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `environment_hazard`
--

DROP TABLE IF EXISTS `environment_hazard`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `environment_hazard` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `ATK` int NOT NULL,
  `DamageType` varchar(20) NOT NULL,
  `CritChance` int NOT NULL,
  `CritDamage` double(2,1) NOT NULL,
  `SpecialEffect` varchar(200) NOT NULL,
  `Dungeon` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `environment_hazard`
--

LOCK TABLES `environment_hazard` WRITE;
/*!40000 ALTER TABLE `environment_hazard` DISABLE KEYS */;
INSERT INTO `environment_hazard` VALUES (1,'Testing Hazard','Hazard for testing purpose',10,'Blunt',10,1.2,'Test Effect','Test Place');
/*!40000 ALTER TABLE `environment_hazard` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `hero`
--

DROP TABLE IF EXISTS `hero`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hero` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `DEF` int NOT NULL,
  `MDEF` int NOT NULL,
  `HP` int NOT NULL,
  `SP` int NOT NULL,
  `MP` int NOT NULL,
  `EXP` int NOT NULL,
  `LVL` int NOT NULL,
  `Weapon` varchar(300) NOT NULL,
  `Armor` varchar(400) NOT NULL,
  `Class` varchar(30) NOT NULL,
  `Skill` varchar(5000) NOT NULL,
  `Magic` varchar(5000) NOT NULL,
  `Passive` varchar(3000) NOT NULL,
  `UserId` int NOT NULL,
  `Race` varchar(100) NOT NULL,
  `Background` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `user_hero_idx` (`UserId`),
  CONSTRAINT `user_hero` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hero`
--

LOCK TABLES `hero` WRITE;
/*!40000 ALTER TABLE `hero` DISABLE KEYS */;
INSERT INTO `hero` VALUES (9,'Greg',2,0,14,14,8,0,1,'Short Bow,Dagger,Unarmed','None,Dirty Cloak,None,Old Travel Boots','Hunter','Powerful Shot,Powerful Cut','Self Care','Merchant,Elf',14,'Elf','Merchant');
/*!40000 ALTER TABLE `hero` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `magic`
--

DROP TABLE IF EXISTS `magic`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `magic` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `ATK` int NOT NULL,
  `DamageType` varchar(20) NOT NULL,
  `CritChance` int NOT NULL,
  `CritDamage` double(2,1) NOT NULL,
  `SpecialEffect` varchar(300) NOT NULL,
  `Range` varchar(5) NOT NULL,
  `MPCost` int NOT NULL,
  `CD` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `magic`
--

LOCK TABLES `magic` WRITE;
/*!40000 ALTER TABLE `magic` DISABLE KEYS */;
INSERT INTO `magic` VALUES (1,'Self Care','A basic healing spell that everyone knows about.',5,'None',1,1.5,'None','None',2,3),(2,'Firebolt','A fiery ball of magic.',15,'Fire',15,1.5,'Burn','Both',2,0),(3,'Divine Smite','A strike of holy magic infused into a weapon',15,'Holy',20,2.0,'None','Melee',3,3);
/*!40000 ALTER TABLE `magic` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `monster`
--

DROP TABLE IF EXISTS `monster`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `monster` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `HP` int NOT NULL,
  `DEF` int NOT NULL,
  `MDEF` int NOT NULL,
  `ATK` int NOT NULL,
  `SP` int NOT NULL,
  `MP` int NOT NULL,
  `Passive` varchar(1000) NOT NULL,
  `Skill` varchar(1000) NOT NULL,
  `Magic` varchar(1000) NOT NULL,
  `Race` varchar(100) NOT NULL,
  `Ai` varchar(40) NOT NULL,
  `Dungeon` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `monster`
--

LOCK TABLES `monster` WRITE;
/*!40000 ALTER TABLE `monster` DISABLE KEYS */;
INSERT INTO `monster` VALUES (1,'TestMonster','testing purpose',100,0,0,20,0,0,'Sword Proficiency,Test Passive','Basic Strike,Advanced Strike','None,None','Goblin','Basic','Test Place'),(2,'TestMonster2','testing purpose',10,100,100,5,20,20,'Test Passive,Test Passive','Basic Strike,Shield','Firebolt','Goblin','Defense','Test Place');
/*!40000 ALTER TABLE `monster` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `npc`
--

DROP TABLE IF EXISTS `npc`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `npc` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) COLLATE utf8mb3_hungarian_ci NOT NULL,
  `Description` text COLLATE utf8mb3_hungarian_ci NOT NULL,
  `DEF` int NOT NULL,
  `MDEF` int NOT NULL,
  `HP` int NOT NULL,
  `SP` int NOT NULL,
  `MP` int NOT NULL,
  `EXP` int NOT NULL,
  `LVL` int NOT NULL,
  `Weapon` varchar(300) COLLATE utf8mb3_hungarian_ci NOT NULL,
  `Armor` varchar(400) COLLATE utf8mb3_hungarian_ci NOT NULL,
  `Class` varchar(30) COLLATE utf8mb3_hungarian_ci NOT NULL,
  `Skill` varchar(5000) COLLATE utf8mb3_hungarian_ci NOT NULL,
  `Magic` varchar(5000) COLLATE utf8mb3_hungarian_ci NOT NULL,
  `Passive` varchar(3000) COLLATE utf8mb3_hungarian_ci NOT NULL,
  `Race` varchar(100) COLLATE utf8mb3_hungarian_ci NOT NULL,
  `Background` varchar(50) COLLATE utf8mb3_hungarian_ci NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_hungarian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `npc`
--

LOCK TABLES `npc` WRITE;
/*!40000 ALTER TABLE `npc` DISABLE KEYS */;
INSERT INTO `npc` VALUES (1,'Gregor','Adventure Buddy',10,10,100,20,20,0,1,'Dagger,Short Bow,Unarmed','None,Worn Leather Chestpiece,Worn Leather Knee Pads,Old Travel Boots','Fighter','Powerful Slash,Powerful Shot,Powerful Stab','Firebolt,Self care','Adventurer,Human','Human','Adventurer');
/*!40000 ALTER TABLE `npc` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `passive`
--

DROP TABLE IF EXISTS `passive`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `passive` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `Affect` varchar(500) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `passive`
--

LOCK TABLES `passive` WRITE;
/*!40000 ALTER TABLE `passive` DISABLE KEYS */;
INSERT INTO `passive` VALUES (1,'None','Not so special now are we.','Nothing'),(2,'Adventurer','Adventurers are self made explorers of the land and its dungeons. They complete quests small and big for rewards from the guilds.','Level Up,Experience Gain,Rest'),(3,'Noble','Nobles are the ruling class of society. They use their lineage to keep themselves in power and strong in therms of magic.','Shop Payment'),(4,'Merchant','Merchants are the ones responsible for keeping the money flowing out here in the valley as well as some supplies that can\'t be found here. Using their channels they can get gold anywhere but not any time.','Sleep,Shop Payment'),(5,'Blacksmith','Blacksmiths make the weapons, armors, tools and many more for the people and their massive bodies forged while forging are a force to be reckoned with.','Damage Calculation,Upgrade Weapon,Upgrade Armor'),(6,'Human','Humans are both adaptive and quick learners.','Experience Gain'),(7,'Elf','Elves have darksight and great mana affinity.','Turn End,Trap Triggered,Initiative Roll'),(8,'Dwarf','Dwarves are physically quite robust and are better at forging than anyone.','Upgrade Weapon,Upgrade Armor,DEF Calculation'),(9,'Halfling','Halflinges are small and brave. They are hard to stop as they are arogant.','Debuff Recieved,After Crit');
/*!40000 ALTER TABLE `passive` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `post`
--

DROP TABLE IF EXISTS `post`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `post` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(100) NOT NULL,
  `Content` text NOT NULL,
  `Date` date NOT NULL,
  `CategoryId` int NOT NULL,
  `PosterId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `user_post_idx` (`PosterId`),
  KEY `category_post_idx` (`CategoryId`),
  CONSTRAINT `category_post` FOREIGN KEY (`CategoryId`) REFERENCES `category` (`Id`),
  CONSTRAINT `user_post` FOREIGN KEY (`PosterId`) REFERENCES `user` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `post`
--

LOCK TABLES `post` WRITE;
/*!40000 ALTER TABLE `post` DISABLE KEYS */;
/*!40000 ALTER TABLE `post` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `race`
--

DROP TABLE IF EXISTS `race`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `race` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `Fatal` varchar(100) NOT NULL,
  `Weak` varchar(100) NOT NULL,
  `Resist` varchar(100) NOT NULL,
  `Endure` varchar(100) NOT NULL,
  `Null` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `race`
--

LOCK TABLES `race` WRITE;
/*!40000 ALTER TABLE `race` DISABLE KEYS */;
INSERT INTO `race` VALUES (1,'Human','The average race we all know.','None','Dark','None','None','None'),(2,'Goblin','The most basic green skinned monster.','None','None','None','None','None'),(3,'Elf','The long lived and frail worshipers of nature.','None','Blunt,Slash,Pierce,Dark','Holy','None','None'),(4,'Dwarf','The short inhabitants of caves with a short temper. They love their alcohol and forging.','None','Ice','Poison,Earth,Thunder','None','None'),(5,'Halfling','The short adventurers of the wild there is no place they wouldn\'t explore.','None','Dark','None','None','None');
/*!40000 ALTER TABLE `race` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `save_game`
--

DROP TABLE IF EXISTS `save_game`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `save_game` (
  `HeroId` int NOT NULL,
  `SaveData` text COLLATE utf8mb3_hungarian_ci NOT NULL,
  `SaveName` varchar(50) COLLATE utf8mb3_hungarian_ci NOT NULL,
  `Id` int NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`Id`),
  KEY `hero_save_idx` (`HeroId`),
  CONSTRAINT `hero_save` FOREIGN KEY (`HeroId`) REFERENCES `hero` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_hungarian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `save_game`
--

LOCK TABLES `save_game` WRITE;
/*!40000 ALTER TABLE `save_game` DISABLE KEYS */;
INSERT INTO `save_game` VALUES (9,'9$0@Greg@0@0@14@14@8@0@1@Short Bow,Dagger,Unarmed@None,Dirty Cloak,None,Old Travel Boots@Powerful Shot,Powerful Cut@Self Care@Merchant,Elf@@Hunter@Elf@Merchant@$Test Item@0$10$0$False$False$0%0%0%0%0%0%0$0%0%0%0%0%0%0$True%False%False%True%True%False%False$True%True%True%True%True%True%True$False','Test',12);
/*!40000 ALTER TABLE `save_game` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `skill`
--

DROP TABLE IF EXISTS `skill`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `skill` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `DamageType` varchar(20) NOT NULL,
  `CritChance` int NOT NULL,
  `CritDamage` double(2,1) NOT NULL,
  `SpecialEffect` varchar(300) NOT NULL,
  `Range` varchar(6) NOT NULL,
  `SPCost` int NOT NULL,
  `CD` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `skill`
--

LOCK TABLES `skill` WRITE;
/*!40000 ALTER TABLE `skill` DISABLE KEYS */;
INSERT INTO `skill` VALUES (1,'Powerful Cut','A powerful cut.','Slash',20,3.0,'None','Melee',2,2),(2,'Powerful Stab','A powerful stab.','Pierce',25,2.5,'None','Melee',2,2),(3,'Powerful Shot','A powerful shot.','Pierce',20,3.0,'None','Ranged',2,2),(4,'Knock Away','A weak strike that forces the enemy to move backwards.','Blunt',1,2.0,'None','Melee',1,2),(6,'Taunt','Makes an enemy focus you on its turn.','None',0,0.0,'Taunt','Both',3,4),(7,'Mark Bounty','Marks an enemy and makes them take extra damage.','None',0,0.0,'Mark','Both',1,4);
/*!40000 ALTER TABLE `skill` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `special_effect`
--

DROP TABLE IF EXISTS `special_effect`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `special_effect` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `Affect` varchar(500) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `special_effect`
--

LOCK TABLES `special_effect` WRITE;
/*!40000 ALTER TABLE `special_effect` DISABLE KEYS */;
INSERT INTO `special_effect` VALUES (1,'None','Not so special now are we.','Nothing'),(2,'Shield','A shield gives you its ATK as DEF when equipped.','Weapon Equip'),(3,'Taunt','Changes the focus of a creature to this one.','Skill Use,Magic Use,Basic Attack'),(4,'Mark','A mark of death that makes the creature take more damage.','Skill Use,Magic Use,Basic Attack'),(5,'Burn','Sets the target on fire.','Skill Use,Magic Use,Basic Attack,Environment Hazard');
/*!40000 ALTER TABLE `special_effect` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserName` varchar(30) NOT NULL,
  `Password` varchar(255) DEFAULT NULL,
  `Email` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserName_UNIQUE` (`UserName`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (5,'BenczeIstvan','Terem35_#','BenczeIstvan@gmail.com'),(13,'kemtam197','$2b$05$bA2nndYSmOQyrmNkFegQA.H5sIi6Wmk13KXOCoYA19oYoUrfPjtiS','kemtam197@hengersor.hu'),(14,'Patrik05','$2b$05$znU95Mg1XAOzLFXi8/emCu2WWTJzdN9MBsl/LOJfOSLJPnL/tz3qG','SabPat702@hengersor.hu');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `weapon`
--

DROP TABLE IF EXISTS `weapon`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `weapon` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `ATK` int NOT NULL,
  `DamageType` varchar(20) NOT NULL,
  `CritChance` int NOT NULL,
  `CritDamage` double(2,1) NOT NULL,
  `SpecialEffect` varchar(300) NOT NULL,
  `Range` varchar(6) NOT NULL,
  `SkillCompatibility` varchar(6) NOT NULL,
  `Price` int NOT NULL,
  `Unigue` tinyint NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `weapon`
--

LOCK TABLES `weapon` WRITE;
/*!40000 ALTER TABLE `weapon` DISABLE KEYS */;
INSERT INTO `weapon` VALUES (1,'Unarmed','No weapons here',2,'Blunt',1,1.1,'None','Melee','Melee',0,0),(2,'Rusty Longsword','A rusty longsword that was probably picked up from the side of the road.',8,'Slash',4,1.5,'None','Melee','Melee',0,0),(3,'Broken Spear','A long spear broken in half that now is used as a short spear.',6,'Pierce',10,1.5,'None','Melee','Melee',0,0),(4,'Short Bow','A short bow with short range and small power behind each shot.',6,'Pierce',10,2.0,'None','Ranged','Ranged',0,0),(5,'Dagger','A small dagger easily hidden but not that strong and effective in frontal combat.',4,'Pierce',15,1.5,'None','Melee','Melee',0,0),(6,'Travel Staff','A staff made with traveling in mind. Not that useful for fighting.',4,'Blunt',4,1.5,'None','Melee','Melee',0,0),(7,'Wooden Shield','A small shield made of wood.',2,'Blunt',1,1.1,'Shield','Melee','Melee',0,0);
/*!40000 ALTER TABLE `weapon` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-03-21 14:16:22
