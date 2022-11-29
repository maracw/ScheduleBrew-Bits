SELECT * FROM bits.batch;
use bits;

INSERT INTO batch (batch_id, recipe_id, equipment_id, volume, 
scheduled_start_date, start_date, estimated_finish_date, finish_date, 
unit_cost, notes, taste_notes, taste_rating, og, fg, carbonation,
 fermentation_stages, primary_age, primary_temp, secondary_age, secondary_temp, tertiary_age, age,
 temp, ibu, ibu_method, abv, actual_efficiency, calories, carbonation_used, forced_carbonation, keg_priming_factor, carbonation_temp) VALUES
(1, 1, 1, 1, '2020-01-01 00:00:00', '2020-01-01 00:00:00', '2020-01-01 00:00:00', '2020-01-01 00:00:00', 
1, 'notes', 'taste note', 1, 3.1, 4.9, 1,
 1, 1, 1, 1, 1, 1, 1, 
 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

