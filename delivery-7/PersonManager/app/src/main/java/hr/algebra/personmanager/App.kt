package hr.algebra.personmanager

import android.app.Application
import hr.algebra.personmanager.dao.PeopleDatabase
import hr.algebra.personmanager.dao.PersonDao

class App : Application() {
    private lateinit var personDao: PersonDao

    override fun onCreate() {
        super.onCreate()
        var db = PeopleDatabase.getInstance(this)
        personDao = db.personDao()
    }

    fun getPersonDao() = personDao

}