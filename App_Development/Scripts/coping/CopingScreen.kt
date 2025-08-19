package com.tus.k00276275_project.coping

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import androidx.navigation.NavHostController
import com.tus.k00276275_project.ui.components.TopLevelScaffold

@Composable
fun CopingScreen(navController: NavHostController) {
    Surface(
        modifier = Modifier
            .fillMaxSize()
            .padding(16.dp) // Adjusted padding for consistency
    ) {
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(16.dp) // Extra padding for column content
        ) {
            Text(
                text = "Tips for those affected by dementia",
                modifier = Modifier.padding(bottom = 16.dp), // Bottom spacing for the title
                style = androidx.compose.material3.MaterialTheme.typography.headlineMedium
            )

            Text(
                text = "If you have recently been diagnosed with dementia, it is normal to experience a wide range of emotions, such as denial, anger, fear, loneliness, frustration, loss, and/or depression.\n" +
                        "\n" +
                        "Here are some things to keep in mind in order to manage the disease in a way that benefits you and your family:",
                modifier = Modifier.padding(bottom = 16.dp), // Bottom spacing for paragraphs
                style = androidx.compose.material3.MaterialTheme.typography.bodyLarge
            )

            Text(
                text = "Take care of your physical health through exercise, adequate rest, and diet\n" +
                        "Schedule regular medical check-ups with professionals who have expertise in dementia and related issues, such as a geriatricians (e.g. doctor specialised in health care of the elderly). If you feel depressed, seek mental health treatment with a professional who has experience in this area.\n" +
                        "Take your medication as prescribed and avoid making changes to your regimen without medical advice.\n" +
                        "Avoid using alcohol as a coping mechanism. While it may make you feel better and distract you, this is only temporarily. In addition, alcohol could interact with medications or cause additional health or memory or language problems.\n" +
                        "Join a dementia support group where you can connect with others and learn more about the disease.\n" +
                        "If you are still working, seek potential work options, such as a reassignment to a more manageable position, early retirement, or other accommodations.\n" +
                        "Keep the lines of communication open with friends, family, and loved ones. Continue to share your feelings with them. Do not hesitate to ask for help when you need it.\n" +
                        "Organise your belongings in such a way that things are easy to find. Try to put things in the same place every time. Putting labels on drawers and doors can be another helpful way of keeping track of things.\n" +
                        "Continue participating in your favourite activities as long as you can and as long as you still enjoy them. Think of ways to modify these activities to adapt to your changing level of functioning. You may need help from a doctor, a family member, or a friend to do this. For example, if reading a long novel becomes difficult, consider listening to shorter segments on tape.\n" +
                        "Perform difficult tasks at times of the day when you feel you are at your best and allow yourself enough time to complete them. If a task becomes too difficult, do not hesitate to take a break or ask for assistance with it\n" +
                        "Keep a written schedule handy to keep track of appointments, tasks, and medication schedules. Also, make a list of important phone numbers as well as your personal information (where you live, your phone number, and emergency contacts) and keep it with you in case of emergencies.\n" +
                        "Consider keeping a journal to write down, express, and work through your feelings.\n" +
                        "Plan for the future. Decide how you want things to be handled when your disease progresses to the point where you can no longer care for yourself. Communicate your medical preferences to your family - especially the closest one - and make necessary legal and financial arrangements (for instance, appointing trusted person to handle your money and affairs when you can no longer do so). Also consider and discuss where you would like to live if alternative housing arrangements become necessary.",
                modifier = Modifier.padding(vertical = 8.dp), // Vertical spacing for paragraphs
                style = androidx.compose.material3.MaterialTheme.typography.bodyLarge
            )
        }
    }
}