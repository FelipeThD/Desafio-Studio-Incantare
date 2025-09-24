CREATE EXTENSION IF NOT EXISTS "pgcrypto";

DROP TABLE IF EXISTS contacts;

CREATE TABLE contacts (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name TEXT NOT NULL,
    email TEXT NOT NULL,
    message TEXT,
    created_at TIMESTAMP(0) DEFAULT CURRENT_TIMESTAMP
);

DROP TABLE IF EXISTS team_members;

CREATE TABLE team_members (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name TEXT NOT NULL,
    role TEXT NOT NULL,
    bio TEXT,
    image_url TEXT,
    created_at TIMESTAMP(0) DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO team_members (name, role, bio)
VALUES
    ('Gustavo Redua', 'Game Design', 'Responsável pelo design de jogos e mecânicas de gameplay.'),
    ('Bruno Neves', 'Frontend Trainee', 'Aprendiz de desenvolvimento frontend, focado em interfaces e UX.'),
    ('Elisoli', 'Admin/Fullstack', 'Gerencia operações e contribui com desenvolvimento fullstack.'),
    ('Fabio', 'Frontend', 'Desenvolvedor frontend, focado em construção de interfaces.'),
    ('Gustavo', 'Frontend', 'Desenvolvedor frontend, trabalhando em projetos web.'),
    ('Luiz', 'CEO/Project Manager/Tech Lead', 'Lidera a equipe e gerencia projetos de tecnologia.'),
    ('Felipe Lima', 'Backend Trainee', 'Aprendiz de backend, trabalhando com APIs e lógica de servidor.');

DROP TABLE IF EXISTS users;

CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    username VARCHAR(150) NOT NULL UNIQUE,
    password_hash TEXT NOT NULL
);
