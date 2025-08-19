package com.tus.k00276275_project.ui.components

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxHeight
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import androidx.navigation.NavHostController
import com.tus.k00276275_project.navigation.Screen

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun NavigationDrawerScaffold(
    navController: NavHostController,
    startDestination: String,
    content: @Composable (PaddingValues) -> Unit
) {
    var isDrawerOpen by remember { mutableStateOf(false) }

    ModalNavigationDrawer(
        drawerContent = {
            DrawerContent(navController) { isDrawerOpen = false }
        },
        gesturesEnabled = isDrawerOpen,
        content = {
            Scaffold(
                topBar = {
                    MainPageTopAppBar(
                        onMenuClick = { isDrawerOpen = true },
                        onBackClick = { isDrawerOpen = false },
                        isDrawerOpen = isDrawerOpen
                    )
                },
                bottomBar = {
                    MainPageNavigationBar(navController)
                },
                content = content
            )
        }
    )
}

@Composable
private fun DrawerContent(
    navController: NavHostController,
    closeDrawer: () -> Unit
) {
    Column(
        modifier = Modifier
            .fillMaxHeight()
            .padding(16.dp)
    ) {
        Button(
            onClick = {
                navController.navigate(Screen.Login.route)
                closeDrawer()
            },
            modifier = Modifier.fillMaxWidth()
        ) {
            Text("Login")
        }
        Spacer(Modifier.padding(8.dp))
        Button(
            onClick = {
                navController.navigate(Screen.Settings.route)
                closeDrawer()
            },
            modifier = Modifier.fillMaxWidth()
        ) {
            Text("Settings")
        }
        Spacer(Modifier.padding(8.dp))
        Button(
            onClick = {
                navController.navigate(Screen.Donate.route)
                closeDrawer()
            },
            modifier = Modifier.fillMaxWidth()
        ) {
            Text("Donate")
        }
        Spacer(Modifier.padding(8.dp))
        Button(
            onClick = {
                navController.navigate(Screen.Newsletter.route)
                closeDrawer()
            },
            modifier = Modifier.fillMaxWidth()
        ) {
            Text("Newsletter")
        }
    }
}