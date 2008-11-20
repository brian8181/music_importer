CREATE PROCEDURE `get_default_art` (sid INTEGER)
BEGIN

SELECT `art`.`file` FROM art JOIN song_art ON art.id=song_art.art_id JOIN song ON song.id=song_art.song_id WHERE song.id=sid LIMIT 1;

END