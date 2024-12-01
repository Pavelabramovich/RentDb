CREATE TABLE IF NOT EXISTS client_colors
(
    owner_id                 uuid NOT NULL,
    client_id                uuid NOT NULL,
    color                    character varying(7) NOT NULL,

    CONSTRAINT PRIMARY KEY (owner_id, client_id),
    CONSTRAINT FOREIGN KEY (client_id)
        REFERENCES individual_profiles (id) MATCH SIMPLE
        ON DELETE CASCADE,

    CONSTRAINT FOREIGN KEY (owner_id)
        REFERENCES legal_person_profiles (id) MATCH SIMPLE
        ON DELETE CASCADE
)