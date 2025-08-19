package com.tus.k00276275_project.donate

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.unit.dp
import androidx.navigation.NavHostController

@Composable
fun DonateScreen(navController: NavHostController) {
    var donationAmount by remember { mutableStateOf("") }
    var thankYouMessage by remember { mutableStateOf("") }

    Surface(
        modifier = Modifier
            .fillMaxSize()
            .padding(16.dp)
    ) {
        Column(
            verticalArrangement = Arrangement.Top,
            modifier = Modifier.fillMaxSize()
        ) {
            Text(
                text = "Support Page",
                style = MaterialTheme.typography.headlineMedium,
                modifier = Modifier.padding(bottom = 16.dp)
            )

            // Text box for input
            OutlinedTextField(
                value = donationAmount,
                onValueChange = { donationAmount = it },
                label = { Text("Enter donation amount") },
                keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Number),
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(bottom = 16.dp)
            )

            // Button
            Button(
                onClick = {
                    thankYouMessage = "Thank you for donating $donationAmount"
                },
                modifier = Modifier.fillMaxWidth()
            ) {
                Text("Donate")
            }

            Spacer(modifier = Modifier.height(16.dp))

            // Display Thank You Message
            if (thankYouMessage.isNotEmpty()) {
                Text(
                    text = thankYouMessage,
                    style = MaterialTheme.typography.bodyLarge,
                    modifier = Modifier.padding(top = 16.dp)
                )
            }
        }
    }
}