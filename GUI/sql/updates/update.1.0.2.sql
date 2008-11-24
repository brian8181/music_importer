DROP PROCEDURE IF EXISTS `get_artist_by_letter`;
CREATE PROCEDURE `get_artist_by_letter`(IN letter CHAR, filter TEXT)

BEGIN

IF letter <> 'T' THEN

SELECT DISTINCT artist.id, artist FROM artist INNER JOIN song ON artist_id=artist.id
  WHERE artist like CONCAT(letter, '%') AND song.file LIKE CONCAT(filter, '%')
    UNION
SELECT artist.id, SUBSTRING(artist.artist, 5, LENGTH(artist.artist) - 1) FROM artist INNER JOIN song ON artist_id=artist.id
    WHERE artist like CONCAT('The ', letter, '%') AND song.file LIKE CONCAT(filter, '%') ORDER BY artist;

ELSE

  SELECT DISTINCT artist.id, artist FROM  artist INNER JOIN song ON artist_id=artist.id
    WHERE ((artist LIKE 'T%' AND artist NOT LIKE 'The %') OR (artist LIKE 'The T%'))
    AND song.file LIKE CONCAT(filter, '%') ORDER BY artist;

END IF;

END
