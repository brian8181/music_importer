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

