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

-- File Properties --
DROP TABLE IF EXISTS `file`;
CREATE TABLE `file` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `file` text collate latin1_german1_ci NOT NULL,
  `path` text collate latin1_german1_ci NOT NULL,
  `codec` text collate latin1_german1_ci default NULL,
  `sample_rate` int(11) default NULL,
  `bitrate` int(11) default NULL,
  `file_size` int(11) default NULL,
  `file_type` text character set latin1,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=36392 DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci PACK_KEYS=1 ROW_FORMAT=DYNAMIC COMMENT='InnoDB';

-- ID3v2.3 Only Properties --
DROP TABLE IF EXISTS `id3v2`;
CREATE TABLE `file` (
  `id` int(10) unsigned NOT NULL auto_increment,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=36392 DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci PACK_KEYS=1 ROW_FORMAT=DYNAMIC COMMENT='InnoDB';

--tag table--
DROP TABLE IF EXISTS `tag`;
CREATE TABLE `tag` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2279 DEFAULT CHARSET=latin1;

--song_tag table--
DROP TABLE IF EXISTS `song_tag`;
CREATE TABLE `song_tag` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2279 DEFAULT CHARSET=latin1;

--update table--
DROP TABLE IF EXISTS `update`;
CREATE TABLE `update` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `update` text collate latin1_german1_ci NOT NULL,
  `version` int(11) default NULL,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2279 DEFAULT CHARSET=latin1;

--Administration--
DROP TABLE IF EXISTS `web_admin`.`group`;
CREATE TABLE  `web_admin`.`group` (
  `id` int(11) NOT NULL auto_increment,
  `group` text,
  `comment` text,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `limits`;
CREATE TABLE  `limits` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `user_id` int(10) unsigned NOT NULL,
  `ip` text NOT NULL,
  `insert_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

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
) ENGINE=MyISAM AUTO_INCREMENT=25 DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `user_group`;
CREATE TABLE `user_group` (
  `id` int(11) NOT NULL auto_increment,
  `user_id` int(11) default NULL,
  `group_id` int(11) default NULL,
  `update_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `insert_ts` timestamp NOT NULL default '0000-00-00 00:00:00',
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=738 DEFAULT CHARSET=latin1;