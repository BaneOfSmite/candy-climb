public class SaveDataFile {
    //Variables
    public float score;
    public int[] Achievements;
    public int sugarRush;
    public int healthyVomit;
    public int totalCollected;
    public int enemiesKilled;

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

    public void sethealthyVomit(int _healthyVomit) {
        this.healthyVomit = _healthyVomit;
    }
    public void settotalCollected(int _totalCollected) {
        this.totalCollected = _totalCollected;
    }
    public void setenemiesKilled(int _enemiesKilled) {
        this.enemiesKilled = _enemiesKilled;
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

    public int gethealthyVomit() {
        return this.healthyVomit;
    }
    public int gettotalCollected() {
        return this.totalCollected;
    }
    public int getenemiesKilled() {
        return this.enemiesKilled;
    }
}
