CREATE TABLE IF NOT EXISTS individual_profiles
(
    id uuid NOT NULL,

    CONSTRAINT PRIMARY KEY (id),

    CONSTRAINT  FOREIGN KEY (id)
        REFERENCES public.profiles (id) MATCH SIMPLE
        ON DELETE CASCADE
)