package com.tus.k00276275_project.ui.components

import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Menu
import androidx.compose.material.icons.filled.ArrowBack
import androidx.compose.material3.CenterAlignedTopAppBar
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.Icon
import androidx.compose.material3.IconButton
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.res.stringResource
import com.tus.k00276275_project.R

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun MainPageTopAppBar(
    isDrawerOpen: Boolean, // State to check if drawer is open
    onMenuClick: () -> Unit, // Menu button click
    onBackClick: () -> Unit // Back button click
) {
    CenterAlignedTopAppBar(
        title = {
            Text(stringResource(id = R.string.app_name))
        },
        navigationIcon = {
            IconButton(
                onClick = {
                    if (isDrawerOpen) {
                        onBackClick() // Handle back click if the drawer is open
                    } else {
                        onMenuClick() // Handle menu click if the drawer is closed
                    }
                }
            ) {
                Icon(
                    imageVector = if (isDrawerOpen) Icons.Filled.ArrowBack else Icons.Filled.Menu,
                    contentDescription = if (isDrawerOpen) stringResource(R.string.nav_back) else stringResource(
                        R.string.nav_drawer_menu
                    )
                )
            }
        }
    )
}