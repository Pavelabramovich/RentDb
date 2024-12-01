CREATE TABLE IF NOT EXISTS announcement_images
(
    image_path                text NOT NULL,
    announcement_id           uuid NOT NULL,
    index integer             NOT NULL,

    CONSTRAINT PRIMARY KEY (image_path),
    CONSTRAINT FOREIGN KEY (announcement_id)
        REFERENCES announcements (id)
        ON DELETE CASCADE
)
