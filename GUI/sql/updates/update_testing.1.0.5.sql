DROP VIEW IF EXISTS `query_view`;
CREATE OR REPLACE ALGORITHM=UNDEFINED DEFINER=`web`@`%`
SQL SECURITY DEFINER VIEW `default_view` AS
SELECT 

	(SELECT `art`.`file` AS `file` FROM (`song_art` JOIN `art` ON((`song_art`.`art_id` = `art`.`id`))) 
		WHERE (`song_art`.`song_id` = `song`.`id`) LIMIT 1) AS `art_file`,
	`song`.`track` AS `track`,
	`song`.`title` AS `title`,
	`album`.`album` AS `album`,
	`artist`.`artist` AS `artist`,
	`song`.`file` AS `song_file`,
	`song`.`id` AS `sid`,
	`user_cart`.`user_id` AS `user_id`,
	`user_cart`.`removed_ts` AS `removed_ts`
	
FROM (((`song` LEFT JOIN `artist` 
	ON((`artist`.`id` = `song`.`artist_id`)))
LEFT JOIN `album` 
	ON((`album`.`id` = `song`.`album_id`)))
LEFT JOIN `user_cart` 
	ON(((`song`.`id` = `user_cart`.`song_id`) 
		AND (ISNULL(`user_cart`.`user_id`) 
		OR ISNULL(`user_cart`.`removed_ts`)))));