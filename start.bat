@echo off
start cmd /c "cd frontend && npm start"
start cmd /c "cd backend/Backend/src/WSChat.Backend.API && dotnet run"
