# 🎮 Photo-Challenge: A Fun Web Game 📸

Welcome to **Photo Challenge**! 🎉 This is a fun game where players take on photo challenges and compete for the best shots in the special challenges (best shot and highest jump) 😎📷

This is hosted on my website [kittyfarren.dev](https://kittyfarren.dev/) using Docker (files included) for a game specific to my friends but there are instuctions included here on how to host the game yourself.

This project is a full-stack web application:
- **Frontend**: Vue.js 🌐
- **Backend**: ASP.NET MVC 🖥️
- **Database**: SQLite 🗄️

## 🚀 Features
- 🌟 **Photo Challenges**: Complete the list of challenges to win the main challenge. Upload your favorite photos and the highest jump to win special competition.
- 🌟 **Host Area**: The host can enter the secure host area to approve/ disapprove photos to see if they complete the challenge. Any dissapprovals are shown to the player in the photo view area.
- 🏆 **Leaderboards**: Compete with friends to get the highest score and win the special catagories!
- 📸 **Upload & Vote**: Share your photos and view them in the viewing area and vote for others' photos in the highest jump/ best photo catgories.
- 🎯 **Simple and fun**: Easy-to-understand mechanics for hours of enjoyment!

## ⚙️ Setup Instructions

### Prerequisites 🛠️
Make sure you have the following installed:
- Npm (for installing frontend dependancies)
- Dotnet (for installing backend dependancies)

### Getting Started 🏁

#### 1️⃣ Clone the Repository
```bash
git clone https://github.com/yourusername/photo-challenge.git
cd photo-challenge
```

#### 2️⃣ Frontend Setup (Vue.js) 🌐

Navigate to the `frontend` folder and install the dependencies:

```bash
cd frontend
npm install
```

Then, run the application:

```bash
npm run
```

#### 3️⃣ Backend Setup (ASP.NET MVC) 🖥️

Navigate to the backend folder and restore dependencies:

```bash
cd backend
dotnet restore
```
Then, run the application:

```bash
dotnet run http
```

#### 4️⃣ Access the Game 🏃‍♀️

Once both the frontend and backend are running, open your browser and go to http://localhost:8080 to play the game! 🎮
🎨 How to Play
  - Submit: Submit photos to the main challenge and the special catagories under your name
  - View: View your photos and others in the viewing area by typing their name. You can check if your photos are approved here.
  - Vote: Vote for the winners of the special catagories. Select your name and your winners.
  - Host: Approve or reject your players pictures for adheering to the challenge list.
  - Compete 🏆: Try to get the highest score on the leaderboard!


#### 🏅 Roadmap

   - 🎉 A three.js winners circle with a custom Blender podium
   - 🎥 A generalised game where the host area includes setting the main and special challenges.
   - Look at hosting a generalise game service for others on my website
