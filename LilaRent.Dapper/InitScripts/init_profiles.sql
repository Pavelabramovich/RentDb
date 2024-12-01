CREATE TABLE IF NOT EXISTS profiles
(
    id                uuid NOT NULL,
    user_id           uuid NOT NULL,
    name              character varying(50) NOT NULL,
    description       character varying(500) NOT NULL,
    image_path        text NOT NULL,
    email character   varying(30)  NOT NULL DEFAULT ''::character varying,
    phone text        NOT NULL DEFAULT ''::text,

    CONSTRAINT PRIMARY KEY (id),

    CONSTRAINT FOREIGN KEY (user_id)
        REFERENCES users (id) 
        ON DELETE CASCADE
)