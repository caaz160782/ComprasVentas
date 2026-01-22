FROM postgres:16

# Variables por defecto (pueden sobrescribirse)
ENV POSTGRES_DB=appdb
ENV POSTGRES_USER=appuser
ENV POSTGRES_PASSWORD=apppassword

# Copiamos el script de inicialización
COPY db.sql /docker-entrypoint-initdb.d/
