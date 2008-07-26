-- Music Importer imports ID3 tags to a MySql database. 
-- (Music Database Creation Scripts) July 24, 2008
-- Copyright (C) 2008  Brian Preston

-- This program is free software: you can redistribute it and/or modify
-- it under the terms of the GNU General Public License as published by
-- the Free Software Foundation, either version 3 of the License, or
-- (at your option) any later version.

-- This program is distributed in the hope that it will be useful,
-- but WITHOUT ANY WARRANTY; without even the implied warranty of
-- MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
-- GNU General Public License for more details.

-- You should have received a copy of the GNU General Public License
-- along with this program.  If not, see <http://www.gnu.org/licenses/>.

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for album
-- ----------------------------
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

-- ----------------------------
-- Table structure for art
-- ----------------------------
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

-- ----------------------------
-- Table structure for artist
-- ----------------------------
DROP TABLE IF EXISTS `artist`;
CREATE TABLE `artist` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `artist` text collate latin1_german1_ci NOT NULL,
  `extra` text character set latin1,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2305 DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci ROW_FORMAT=FIXED;

-- ----------------------------
-- Table structure for playlist_songs
-- ----------------------------
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

-- ----------------------------
-- Table structure for playlists
-- ----------------------------
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

-- ----------------------------
-- Table structure for playlists
-- ----------------------------
DROP TABLE IF EXISTS `location`;
CREATE TABLE `location` (
  `id` int(11) NOT NULL auto_increment,
  `path` text,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
   PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=49 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for song
-- ----------------------------
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
  `beats_per_minute` int(10) unsigned NOT NULL,
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
) ENGINE=InnoDB AUTO_INCREMENT=36392 DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci PACK_KEYS=1 ROW_FORMAT=DYNAMIC COMMENT='InnoDB';

-- ----------------------------
-- Table structure for query_log
-- ----------------------------

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
) ENGINE=MyISAM AUTO_INCREMENT=13625 DEFAULT CHARSET=latin1;

-- CREATE TRIGGER `inserted_author_ts` BEFORE INSERT ON `album` FOR EACH ROW SET NEW.insert_ts = NOW(), NEW.update_ts = '0000-00-00 00:00:00';
-- CREATE TRIGGER `inserted_artist_ts` BEFORE INSERT ON `artist` FOR EACH ROW SET NEW.insert_ts = NOW(), NEW.update_ts = '0000-00-00 00:00:00';
-- CREATE TRIGGER `inserted_song_ts` BEFORE INSERT ON `song` FOR EACH ROW SET NEW.insert_ts = NOW(), NEW.update_ts = '0000-00-00 00:00:00';

-- END --