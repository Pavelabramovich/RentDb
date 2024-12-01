CREATE TABLE IF NOT EXISTS legal_person_profiles
(
    id uuid           NOT NULL,
    address           character varying(70) COLLATE pg_catalog."default",
    floor             integer,
    station           character varying(70) COLLATE pg_catalog."default",
    area              numeric,

    CONSTRAINT PRIMARY KEY (id),

    CONSTRAINT FOREIGN KEY (id)
        REFERENCES profiles (id) MATCH SIMPLE
        ON DELETE CASCADE
)
