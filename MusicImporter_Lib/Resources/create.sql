DROP TABLE IF EXISTS `album`;
CREATE TABLE `album` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `album` text collate latin1_german1_ci NOT NULL,
  `artist` text collate latin1_german1_ci,
  `art` text collate latin1_german1_ci,
  `extra` text collate latin1_german1_ci,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2901 DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci ROW_FORMAT=DYNAMIC;

CREATE TRIGGER `inserted_album_ts` BEFORE INSERT ON `album` FOR EACH ROW SET NEW.insert_ts = NOW(), NEW.update_ts = '0000-00-00 00:00:00';

DROP TABLE IF EXISTS `art`;
CREATE TABLE `art` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `file` text,
  `type` text,
  `hash` blob,
  `description` text,
  `mime_type` text,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2279 DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `artist`;
CREATE TABLE `artist` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `artist` text collate latin1_german1_ci NOT NULL,
  `extra` text character set latin1,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2305 DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci ROW_FORMAT=FIXED;

CREATE TRIGGER `inserted_artist_ts` BEFORE INSERT ON `artist` FOR EACH ROW SET NEW.insert_ts = NOW(), NEW.update_ts = '0000-00-00 00:00:00';

DROP TABLE IF EXISTS `download`;
CREATE TABLE `download` (
  `id` int(11) NOT NULL auto_increment,
  `song_id` int(11) default NULL,
  `user_id` int(11) default NULL,
  `insert_ts` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `group`;
CREATE TABLE `group` (
  `id` int(11) NOT NULL auto_increment,
  `group` text,
  `comment` text,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `limits`;
CREATE TABLE `limits` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `user_id` int(10) unsigned NOT NULL,
  `ip` text NOT NULL,
  `type` enum('CREATE','FAIL_LOGIN') NOT NULL default 'CREATE',
  `insert_ts` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `location`;
CREATE TABLE `location` (
  `id` int(11) NOT NULL auto_increment,
  `path` text,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=49 DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `login`;
CREATE TABLE `login` (
  `id` int(11) NOT NULL auto_increment,
  `user_id` int(11) default NULL,
  `insert_ts` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=738 DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `playlist_songs`;
CREATE TABLE `playlist_songs` (
  `id` int(11) NOT NULL auto_increment,
  `playlist_id` int(11) default NULL,
  `song_id` int(11) default NULL,
  `order` int(10) unsigned default NULL,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`),
  KEY `Index_play_id` (`playlist_id`),
  KEY `Index_song_id` (`song_id`)
) ENGINE=InnoDB AUTO_INCREMENT=881 DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `playlists`;
CREATE TABLE `playlists` (
  `id` int(11) NOT NULL auto_increment,
  `name` text,
  `user_id` int(11) default NULL,
  `masked` tinyint(3) unsigned NOT NULL default '0',
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=49 DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `query_log`;
CREATE TABLE `query_log` (
  `id` int(11) NOT NULL auto_increment,
  `query_type` text,
  `album` text,
  `artist` text,
  `title` text,
  `genre` text,
  `file` text,
  `comments` text,
  `lyrics` text,
  `and` tinyint(1) default '1',
  `wildcard` tinyint(1) default '0',
  `sortby` text,
  `ip` text,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `security_question`;
CREATE TABLE `security_question` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `question` text NOT NULL,
  `default` tinyint(1) NOT NULL default '0',
  `insert_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

INSERT INTO `security_question` VALUES  (1,'What is your favorite pets name?',1,'2008-11-18 13:24:35'),
 (2,'What is your favorite singers name?',1,'2008-11-18 13:24:35'),
 (3,'What is your mother madien name?',1,'2008-11-18 13:24:36');

DROP TABLE IF EXISTS `song`;
CREATE TABLE `song` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `artist_id` int(10) unsigned default NULL,
  `album_id` int(10) unsigned default NULL,
  `track` int(11) default NULL,
  `title` text collate latin1_german1_ci,
  `file` text collate latin1_german1_ci NOT NULL,
  `genre` text collate latin1_german1_ci,
  `bitrate` int(11) default NULL,
  `length` text character set latin1,
  `year` year(4) default NULL,
  `comments` text collate latin1_german1_ci,
  `encoder` text character set latin1,
  `file_size` int(11) default NULL,
  `file_type` text character set latin1,
  `art_id` int(10) unsigned default NULL,
  `lyrics` text collate latin1_german1_ci,
  `composer` text collate latin1_german1_ci,
  `conductor` text collate latin1_german1_ci,
  `copyright` text collate latin1_german1_ci,
  `disc` int(10) unsigned default NULL,
  `disc_count` int(10) unsigned default NULL,
  `performer` text collate latin1_german1_ci,
  `tag_types` text collate latin1_german1_ci,
  `track_count` int(10) unsigned default NULL,
  `beats_per_minute` int(10) unsigned default NULL,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`),
  KEY `idx_artist_id` (`artist_id`),
  KEY `idx_album_id` (`album_id`),
  KEY `idx_title` (`title`(25)),
  KEY `idx_art_id` (`art_id`),
  CONSTRAINT `FK_song_1` FOREIGN KEY (`album_id`) REFERENCES `album` (`id`),
  CONSTRAINT `FK_song_2` FOREIGN KEY (`artist_id`) REFERENCES `artist` (`id`),
  CONSTRAINT `FK_song_3` FOREIGN KEY (`art_id`) REFERENCES `art` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci PACK_KEYS=1 ROW_FORMAT=DYNAMIC COMMENT='InnoDB';

CREATE TRIGGER `inserted_song_ts` BEFORE INSERT ON `song` FOR EACH ROW SET NEW.insert_ts = NOW(), NEW.update_ts = '0000-00-00 00:00:00';

DROP TABLE IF EXISTS `song_art`;
CREATE TABLE `song_art` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `song_id` int(10) unsigned NOT NULL,
  `art_id` int(10) unsigned NOT NULL,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`),
  KEY `Index_song_id` (`song_id`),
  KEY `Index_art_id` (`art_id`),
  CONSTRAINT `FK_song` FOREIGN KEY (`song_id`) REFERENCES `song` (`id`),
  CONSTRAINT `FK_art` FOREIGN KEY (`art_id`) REFERENCES `art` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TRIGGER `inserted_song_art_ts` BEFORE INSERT ON `song_art` FOR EACH ROW SET NEW.insert_ts = NOW(), NEW.update_ts = '0000-00-00 00:00:00';

DROP TABLE IF EXISTS `song_tag`;
CREATE TABLE `song_tag` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `style`;
CREATE TABLE `style` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `style` text NOT NULL,
  `file` text NOT NULL,
  `insert_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `updated_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

INSERT INTO `style` VALUES  (4,'Default','./css/style.css','2008-11-18 13:24:36','0000-00-00 00:00:00'),
 (5,'Blue','./css/blue.css',NOW(),'0000-00-00 00:00:00'),
 (6,'Green','./css/green.css',NOW(),'0000-00-00 00:00:00');

 DROP TABLE IF EXISTS `tag`;
CREATE TABLE `tag` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `update`;
CREATE TABLE `update` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `update` text character set latin1 collate latin1_german1_ci NOT NULL,
  `version` int(11) default NULL,
  `release_date` date default NULL,
  `insert_ts` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

INSERT INTO `update` VALUES  (2,'1.0.0',1,NULL,'2008-11-18 13:24:37');

DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `id` int(11) NOT NULL auto_increment,
  `user` text NOT NULL,
  `full_name` text,
  `password` text,
  `email` text,
  `last_login` datetime default NULL,
  `comment` text,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`),
  UNIQUE KEY `user_idx` (`user`(20))
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `user_cart`;
CREATE TABLE `user_cart` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `user_id` int(11) NOT NULL,
  `song_id` int(11) NOT NULL,
  `removed_ts` datetime default NULL,
  `insert_ts` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `user_group`;
CREATE TABLE `user_group` (
  `id` int(11) NOT NULL auto_increment,
  `user_id` int(11) default NULL,
  `group_id` int(11) default NULL,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`),
  KEY `FK_user_id` (`user_id`),
  KEY `FK_group_id` (`group_id`),
  CONSTRAINT `FK_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`),
  CONSTRAINT `FK_group_id` FOREIGN KEY (`group_id`) REFERENCES `group` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=738 DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `user_security_question`;
CREATE TABLE `user_security_question` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `user_id` int(10) unsigned NOT NULL,
  `question_id` int(10) unsigned NOT NULL,
  `answer` text NOT NULL,
  `insert_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `update_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `user_setting`;
CREATE TABLE `user_setting` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `user_id` int(10) unsigned NOT NULL,
  `style` text NOT NULL,
  `updated_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1

