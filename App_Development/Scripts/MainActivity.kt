package com.tus.k00276275_project

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.activity.enableEdgeToEdge
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Surface
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.tus.k00276275_project.coping.CopingScreen
import com.tus.k00276275_project.helping.HelpingScreen
import com.tus.k00276275_project.navigation.Screen
import com.tus.k00276275_project.support.SupportScreen
import com.tus.k00276275_project.ui.home.HomeScreen
import com.tus.k00276275_project.login.LoginScreen
import com.tus.k00276275_project.settings.SettingsScreen
import com.tus.k00276275_project.donate.DonateScreen
import com.tus.k00276275_project.newsletter.NewsletterScreen
import com.tus.k00276275_project.ui.components.NavigationDrawerScaffold
import com.tus.k00276275_project.ui.components.TopLevelScaffold
import com.tus.k00276275_project.ui.theme.K00276275ProjectTheme

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContent {
            K00276275ProjectTheme {
                Surface(modifier = Modifier.fillMaxSize()) {
                    BuildNavigationGraph()
                }
            }
        }
    }
}

@Composable
private fun BuildNavigationGraph() {
    val navController = rememberNavController()

    TopLevelScaffold(navController = navController) { innerPadding ->
        NavHost(
            navController = navController,
            startDestination = Screen.Home.route,
            modifier = Modifier.padding(innerPadding)
        ) {
            // Bottom Bar Screens
            composable(Screen.Home.route) { HomeScreen(navController) }
            composable(Screen.Coping.route) { CopingScreen(navController) }
            composable(Screen.Helping.route) { HelpingScreen(navController) }
            composable(Screen.Support.route) { SupportScreen(navController) }

            // Drawer Screens
            composable(Screen.Login.route) { LoginScreen(navController) }
            composable(Screen.Settings.route) { SettingsScreen(navController) }
            composable(Screen.Donate.route) { DonateScreen(navController) }
            composable(Screen.Newsletter.route) { NewsletterScreen(navController) }
        }
    }
}