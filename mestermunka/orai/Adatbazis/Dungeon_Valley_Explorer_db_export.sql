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
  `Id` int NOT NULL,
  `Name` varchar(40) NOT NULL,
  `Description` text NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ai`
--

LOCK TABLES `ai` WRITE;
/*!40000 ALTER TABLE `ai` DISABLE KEYS */;
INSERT INTO `ai` VALUES (0,'Basic','The most basic behaviour'),(1,'Defense','Defensive behaviour');
/*!40000 ALTER TABLE `ai` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `armor`
--

DROP TABLE IF EXISTS `armor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `armor` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `DEF` int NOT NULL,
  `MDEF` int NOT NULL,
  `SpecialEffect` varchar(300) NOT NULL,
  `Type` int NOT NULL,
  `Price` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `armor`
--

LOCK TABLES `armor` WRITE;
/*!40000 ALTER TABLE `armor` DISABLE KEYS */;
INSERT INTO `armor` VALUES (0,'Test Helmet','Armor for testing purpose',4,4,'None',1,10),(1,'Test Chestplate','Armor for testing purpose',6,6,'None',2,20),(2,'Test Leggings','Armor for testing purpose',2,2,'None',3,5),(3,'Test Boots','Armor for testing purpose',2,2,'None',4,5);
/*!40000 ALTER TABLE `armor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `buff_and_debuff`
--

DROP TABLE IF EXISTS `buff_and_debuff`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `buff_and_debuff` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `Affect` varchar(500) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `buff_and_debuff`
--

LOCK TABLES `buff_and_debuff` WRITE;
/*!40000 ALTER TABLE `buff_and_debuff` DISABLE KEYS */;
INSERT INTO `buff_and_debuff` VALUES (0,'Damage up','A small increase in damage.','Damage Calculation');
/*!40000 ALTER TABLE `buff_and_debuff` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `category`
--

DROP TABLE IF EXISTS `category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `category` (
  `Id` int NOT NULL,
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
  `Id` int NOT NULL,
  `Content` text NOT NULL,
  `Date` date NOT NULL,
  `CommenterId` int NOT NULL,
  `PostId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `comment_user_idx` (`CommenterId`),
  KEY `comment_post_idx` (`PostId`),
  CONSTRAINT `comment_post` FOREIGN KEY (`PostId`) REFERENCES `post` (`Id`),
  CONSTRAINT `comment_user` FOREIGN KEY (`CommenterId`) REFERENCES `user` (`Id`)
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
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `SpecialEffect` varchar(200) NOT NULL,
  `Price` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `consumable`
--

LOCK TABLES `consumable` WRITE;
/*!40000 ALTER TABLE `consumable` DISABLE KEYS */;
INSERT INTO `consumable` VALUES (0,'Test Item','Item for testin purpose','None',10);
/*!40000 ALTER TABLE `consumable` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dungeon`
--

DROP TABLE IF EXISTS `dungeon`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dungeon` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `Length` int NOT NULL,
  `Gold` int NOT NULL,
  `Experience` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dungeon`
--

LOCK TABLES `dungeon` WRITE;
/*!40000 ALTER TABLE `dungeon` DISABLE KEYS */;
INSERT INTO `dungeon` VALUES (0,'Test Place','A place for testing',10,10,10);
/*!40000 ALTER TABLE `dungeon` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `environment_hazard`
--

DROP TABLE IF EXISTS `environment_hazard`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `environment_hazard` (
  `Id` int NOT NULL,
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `environment_hazard`
--

LOCK TABLES `environment_hazard` WRITE;
/*!40000 ALTER TABLE `environment_hazard` DISABLE KEYS */;
INSERT INTO `environment_hazard` VALUES (0,'Testing Hazard','Hazard for testing purpose',10,'Blunt',10,1.2,'Test Effect','Test Place');
/*!40000 ALTER TABLE `environment_hazard` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `hero`
--

DROP TABLE IF EXISTS `hero`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hero` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
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
  PRIMARY KEY (`Id`),
  KEY `hero_user_idx` (`UserId`),
  CONSTRAINT `hero_user` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hero`
--

LOCK TABLES `hero` WRITE;
/*!40000 ALTER TABLE `hero` DISABLE KEYS */;
INSERT INTO `hero` VALUES (1,'player','Player made hero',0,0,10,10,10,0,1,'Unarmed,Unarmed,Unarmed','Test Helmet, Test Chestplate,Test Leggings,Test Boots','Fighter','Power Slash','Firebolt,Self care','Sword Proficiency',1,'Human');
/*!40000 ALTER TABLE `hero` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `magic`
--

DROP TABLE IF EXISTS `magic`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `magic` (
  `Id` int NOT NULL,
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `magic`
--

LOCK TABLES `magic` WRITE;
/*!40000 ALTER TABLE `magic` DISABLE KEYS */;
INSERT INTO `magic` VALUES (0,'Firebolt','A fiery ball of magic.',15,'Fire',15,1.5,'Burn','Both',2,0),(1,'Self care','A basic healing spell that all adventurers know about.',0,'None',0,0.0,'None','None',2,3);
/*!40000 ALTER TABLE `magic` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `monster`
--

DROP TABLE IF EXISTS `monster`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `monster` (
  `Id` int NOT NULL,
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `monster`
--

LOCK TABLES `monster` WRITE;
/*!40000 ALTER TABLE `monster` DISABLE KEYS */;
INSERT INTO `monster` VALUES (0,'TestMonster','testing purpose',100,0,0,20,0,0,'Sword Proficiency,Test Passive','Basic Strike,Advanced Strike','None,None','Goblin','Basic','Test Place'),(1,'TestMonster2','testing purpose',10,100,100,5,20,20,'Test Passive,Test Passive','Basic Strike,Shield','Firebolt','Goblin','Defense','Test Place');
/*!40000 ALTER TABLE `monster` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `npc`
--

DROP TABLE IF EXISTS `npc`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `npc` (
  `Id` int NOT NULL,
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
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_hungarian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `npc`
--

LOCK TABLES `npc` WRITE;
/*!40000 ALTER TABLE `npc` DISABLE KEYS */;
INSERT INTO `npc` VALUES (0,'Test NPC','For testing purpose',10,10,100,20,20,0,1,'TestWeapon,Unarmed,Unarmed','Test Helmet,Test Chestplate,Test Leggings,Test Boots','Fighter','Power Slash','Firebolt,Self care','Sword Proficiency','Human');
/*!40000 ALTER TABLE `npc` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `passive`
--

DROP TABLE IF EXISTS `passive`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `passive` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `Affect` varchar(500) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `passive`
--

LOCK TABLES `passive` WRITE;
/*!40000 ALTER TABLE `passive` DISABLE KEYS */;
INSERT INTO `passive` VALUES (0,'Sword Proficiency','Sword strikes are a little bit stronger.','Damage Calculation'),(1,'Test Passive','A passive ability for testing purpose.','Nothing'),(2,'None','Not so special now are we.','Nothing');
/*!40000 ALTER TABLE `passive` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `post`
--

DROP TABLE IF EXISTS `post`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `post` (
  `Id` int NOT NULL,
  `Title` varchar(100) NOT NULL,
  `Content` text NOT NULL,
  `Date` date NOT NULL,
  `CategoryId` int NOT NULL,
  `PosterId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `post_user_idx` (`PosterId`),
  KEY `post_category_idx` (`CategoryId`),
  CONSTRAINT `post_category` FOREIGN KEY (`CategoryId`) REFERENCES `category` (`Id`),
  CONSTRAINT `post_user` FOREIGN KEY (`PosterId`) REFERENCES `user` (`Id`)
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
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `Fatal` varchar(100) NOT NULL,
  `Weak` varchar(100) NOT NULL,
  `Resist` varchar(100) NOT NULL,
  `Endure` varchar(100) NOT NULL,
  `Null` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `race`
--

LOCK TABLES `race` WRITE;
/*!40000 ALTER TABLE `race` DISABLE KEYS */;
INSERT INTO `race` VALUES (0,'Human','The average race we all know.','None','None','None','None','None'),(1,'Goblin','The most basic green skinned monster.','None','None','None','None','None');
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
  KEY `hero_save_idx` (`HeroId`),
  CONSTRAINT `hero_save` FOREIGN KEY (`HeroId`) REFERENCES `hero` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_hungarian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `save_game`
--

LOCK TABLES `save_game` WRITE;
/*!40000 ALTER TABLE `save_game` DISABLE KEYS */;
INSERT INTO `save_game` VALUES (1,'placeholdertext','first_save');
/*!40000 ALTER TABLE `save_game` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `skill`
--

DROP TABLE IF EXISTS `skill`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `skill` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `DamageType` varchar(20) NOT NULL,
  `CritChance` int NOT NULL,
  `CritDamage` double(2,1) NOT NULL,
  `SpecialEffect` varchar(300) NOT NULL,
  `Range` varchar(5) NOT NULL,
  `SPCost` int NOT NULL,
  `CD` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `skill`
--

LOCK TABLES `skill` WRITE;
/*!40000 ALTER TABLE `skill` DISABLE KEYS */;
INSERT INTO `skill` VALUES (0,'Basic Strike','A basic strike from the monster','Blunt',10,2.0,'None','Melee',0,0),(1,'Advanced Strike','An advanced striking technique from a monster.','Blunt',15,2.2,'None','Melee',0,0),(2,'Shield','A basic defensive ability that raises the users defense.','None',0,0.0,'None','None',5,2),(3,'Power Slash','A powerful cut.','Slash',20,3.0,'None','Melee',3,2);
/*!40000 ALTER TABLE `skill` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `special_effect`
--

DROP TABLE IF EXISTS `special_effect`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `special_effect` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `Affect` varchar(500) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `special_effect`
--

LOCK TABLES `special_effect` WRITE;
/*!40000 ALTER TABLE `special_effect` DISABLE KEYS */;
INSERT INTO `special_effect` VALUES (0,'Piercing Blade','A blade that can even cut armor. (Ignores a set amount of defense(DEF))','Damage Calculation'),(1,'Burn','Sets your targets ablaze.','Give Buff or Debuff'),(2,'None','Not so special now are we.','Nothing');
/*!40000 ALTER TABLE `special_effect` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `Id` int NOT NULL,
  `UserName` varchar(30) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `Email` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserName_UNIQUE` (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'Patrik','1234','p@e.hu');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `weapon`
--

DROP TABLE IF EXISTS `weapon`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `weapon` (
  `Id` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` text NOT NULL,
  `ATK` int NOT NULL,
  `DamageType` varchar(20) NOT NULL,
  `CritChance` int NOT NULL,
  `CritDamage` double(2,1) NOT NULL,
  `SpecialEffect` varchar(300) NOT NULL,
  `Range` varchar(5) NOT NULL,
  `SkillCompatibility` varchar(5) NOT NULL,
  `Price` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `weapon`
--

LOCK TABLES `weapon` WRITE;
/*!40000 ALTER TABLE `weapon` DISABLE KEYS */;
INSERT INTO `weapon` VALUES (0,'TestWeapon','A weapon for testing.',20,'Slash',10,2.0,'Piercing Blade','Melee','Melee',20),(1,'Unarmed','No weapons here',2,'Blunt',1,1.1,'None','Melee','Melee',0);
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

-- Dump completed on 2025-03-03  9:59:17
