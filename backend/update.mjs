// backend/update.mjs
import { execSync } from 'child_process';
import readline from 'readline';

// 📌 Leggi l'ambiente dal primo argomento (es: dev, test, prod)
const env = process.argv[2];

if (!env) {
  console.error('❌ Specifica un ambiente (es: dev, test, prod).\nEsempio: node update.mjs dev');
  process.exit(1);
}

console.log(`🌍 Ambiente selezionato: ${env}`);
process.env.ASPNETCORE_ENVIRONMENT = env;

// 🖊️ Prompt per il nome della migration
const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout
});

rl.question('📦 Inserisci il nome della migration: ', (migrationNameInput) => {
  const migrationName = migrationNameInput.trim();

  if (!migrationName) {
    console.error('❌ Nome migration non valido.');
    rl.close();
    process.exit(1);
  }

  try {
    console.log(`🔧 Aggiungo migration: ${migrationName}...`);
    execSync(
      `dotnet ef migrations add ${migrationName} --context ApplicationDbContext`,
      {
        stdio: 'inherit',
        cwd: process.cwd(),
        env: process.env
      }
    );

    console.log('🛠️  Eseguo update del database...');
    execSync(
      `dotnet ef database update --context ApplicationDbContext`,
      {
        stdio: 'inherit',
        cwd: process.cwd(),
        env: process.env
      }
    );

    console.log('✅ Operazione completata con successo!');
  } catch (err) {
    console.error('❌ Errore durante esecuzione:', err.message);
  }

  rl.close();
});
