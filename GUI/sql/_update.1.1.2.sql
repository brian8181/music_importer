#ALTER TABLE song DROP COLUMN art_id;
 
DROP TABLE IF EXISTS `song_art`;
CREATE TABLE `song_tag` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `song_id` int NOT NULL,
  `art_id` int NOT NULL,
  `insert_ts` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=latin1;