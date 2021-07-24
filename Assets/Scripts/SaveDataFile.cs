public class SaveDataFile {
    //Variables
    public float score;
    public int[] Achievements;
    public int sugarRush;
    public int healthyVomit;
    public int totalCollected;
    public int enemiesKilled;

    public int music;
    public int effect;

    //Set Functions
    public void setScore(float _score) {
        this.score = _score;
    }
    public void setAchievements(int[] _Achievements) {
        this.Achievements = _Achievements;
    }
    public void setSugarRush(int _sugarRush) {
        this.sugarRush = _sugarRush;
    }

    public void setHealthyVomit(int _healthyVomit) {
        this.healthyVomit = _healthyVomit;
    }
    public void setTotalCollected(int _totalCollected) {
        this.totalCollected = _totalCollected;
    }
    public void setEnemiesKilled(int _enemiesKilled) {
        this.enemiesKilled = _enemiesKilled;
    }
    public void setMusic(int _music) {
        this.music = _music;
    }
    public void setEffect(int _effect) {
        this.effect = _effect;
    }

    //Get Functions
    public float getScore() {
        return this.score;
    }
    public int[] getAchievements() {
        return this.Achievements;
    }
    public int getSugarRush() {
        return this.sugarRush;
    }

    public int getHealthyVomit() {
        return this.healthyVomit;
    }
    public int getTotalCollected() {
        return this.totalCollected;
    }
    public int getEnemiesKilled() {
        return this.enemiesKilled;
    }
    public int getMusic() {
        return this.music;
    }
    public int getEffect() {
        return this.effect;
    }
}
