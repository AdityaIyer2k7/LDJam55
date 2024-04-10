# LDJam55
A repository tracking the development of our entry for Ludum Dare 55 under the Jam category

## To collaborators:
### How to set up Unity
- Enter into the command line (`cmd`, `bash`, etc.).
- Go to the directory where you want to create the game folder (e.g. `C:\Users\HP\Games\`).
- Download the git repo using the following command:

```
git clone https://github.com/AdityaIyer2k7/LDJam55.git
```

- Open Unity Hub (3.7.0) on your device. Sign in using your Unity ID if required.
- Install Unity Engine 2022.3.23f1 from the `Installs` tab on the left (optional: install Android, iOS, IL2CPP, and WebGL build support).
- Navigate to the `Projects` tab on the left and click the `Add ` button in the top-right of the screen.
- Select the directory where you have cloned the repository (e.g. `C:\Users\HP\Games\LDJam55`) and add that as your project.

### How to set up Git CLI (after Unity)
- Enter into the command line (`cmd`, `bash`, etc.).
- Go to the game folder (e.g. `C:\Users\HP\Games\LDJam55`)
- Run the following commands:

```
git login
git remote add origin https://github.com/AdityaIyer2k7/LDJam55.git
git pull origin
```

- Use this command to enter into your working tree

```
git branch --set-upstream-to origin workingtree_YOUR_INITIALS_HERE
```

- To commit changes, run:

```
git add *
git commit -m "YOUR MESSAGE HERE"
```

- To safely push changes, run:

```
git push
```
