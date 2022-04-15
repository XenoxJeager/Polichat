@echo off
start cmd /c "cd frontend && npm start"
start cmd /C "cd backend/Backend/src/WSChat.Backend.API && dotnet run"
