CREATE TABLE tasks (
    id SERIAL PRIMARY KEY,                        -- Уникальный идентификатор задачи
    title VARCHAR(255) NOT NULL,                  -- Название задачи
    description TEXT,                             -- Описание задачи
    due_date DATE,                                -- Срок выполнения задачи
    priority INT,                                 -- Приоритет задачи (например, 1 - высокий, 2 - средний, 3 - низкий)
    category_id INT REFERENCES categories(id),    -- Ссылка на категорию задачи (личная, рабочая и т.д.)
    status VARCHAR(50) DEFAULT 'pending',         -- Статус задачи (pending - в ожидании, completed - выполнена)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP, -- Дата создания задачи
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Дата последнего обновления задачи
);

CREATE TABLE categories (
    id SERIAL PRIMARY KEY,                        -- Уникальный идентификатор категории
    name VARCHAR(100) NOT NULL,                   -- Название категории (например, личные, рабочие и т.д.)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP -- Дата создания категории
);

CREATE TABLE subtasks (
    id SERIAL PRIMARY KEY,                        -- Уникальный идентификатор подзадачи
    task_id INT REFERENCES tasks(id) ON DELETE CASCADE, -- Ссылка на основную задачу
    title VARCHAR(255) NOT NULL,                  -- Название подзадачи
    status VARCHAR(50) DEFAULT 'pending',         -- Статус подзадачи (pending, completed)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP, -- Дата создания подзадачи
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- Дата последнего обновления подзадачи
);

CREATE TABLE notifications (
    id SERIAL PRIMARY KEY,                        -- Уникальный идентификатор уведомления
    task_id INT REFERENCES tasks(id) ON DELETE CASCADE, -- Ссылка на задачу
    notify_at TIMESTAMP NOT NULL,                 -- Время, когда нужно отправить уведомление
    sent BOOLEAN DEFAULT FALSE,                   -- Флаг отправки уведомления (true - отправлено, false - не отправлено)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP -- Дата создания уведомления
);

CREATE TABLE statistics (
    id SERIAL PRIMARY KEY,                        -- Уникальный идентификатор записи
    task_id INT REFERENCES tasks(id) ON DELETE CASCADE, -- Ссылка на задачу
    completed_at TIMESTAMP,                       -- Время завершения задачи
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP -- Дата создания записи
);

-- Индексы для оптимизации
CREATE INDEX idx_task_due_date ON tasks(due_date);
CREATE INDEX idx_task_priority ON tasks(priority);
CREATE INDEX idx_task_status ON tasks(status);