# Flipped-Launcher OSS

take that skid!
![image](https://github.com/Mstreemy/Flipped-Launcher/assets/129096942/766f8933-e953-47af-98dd-7c7ae87c5c11)

to get this to work for your project you need:

- a brain

- a few other changs to files like register.js, functions.js, and user.js

- this api!

```import express from "express";
import bcrypt from "bcrypt";
import User from "../model/user.js";
import functions from "../utilities/structs/functions.js";
import log from "../utilities/structs/log.js";
import Profiles from "../model/profiles.js";
import axios from "axios";
const app = express.Router();

app.get("/fetch/version", (req, res) => {
    res.status(200).json({
        version: "0.1.3"
    });
});

function sendData(block) {
    return async function (req, res) {
        try {
            const result = await block(req, res);
            if (!res.headersSent) {
                res.send(result);
            }
        } catch (error) {
            log.error(error);
            if (!res.headersSent) {
                res.status(500).send("Internal Server Error");
            }
        }
    };
}

app.get('/launcher/loginToken', async (req, res) => {
    const { token } = req.query;

    try {
        const user = await User.findOne({ token });

        if (!user) {
            return res.status(500).json({ error: "real" });
        }

        return res.status(200).send();
    } catch (error) {
        return res.status(500).json({ error: "Server error" });
    }
});

app.get('/launcher/loginHWID', async (req, res) => {
    const { discordId, hwid } = req.query;

    try {
        const updatedUser = await User.findOneAndUpdate(
            { discordId },
            { $set: { hwid } },
            { new: true }
        );

        if (!updatedUser) {
            return res.status(404).json({ error: "User not found" });
        }

        return res.status(200).send();
    } catch (error) {
        return res.status(500).json({ error: error.message });
    }
});

app.get('/launcher/checkhwid', async (req, res) => {
    const { hwid } = req.query;

    if (!hwid) {
        return res.status(400).json({ error: 'you need hwid bud' });
    }

    try {
        const user = await User.findOne({ hwid });

        if (!user) {
            return res.status(404).json();
        }

        if (user.banned) {
          return res.status(200).json("banned");
        }

        const discordId = user.discordId;
        return res.status(200).json(discordId);
    } catch (error) {
        return res.status(500).json({ error: error.message });
    }
});


app.get('/launcher/getDiscordId', async (req, res) => {
    const { token } = req.query;

    try {
        const user = await User.findOne({ token });

        if (!user) {
            return res.status(404).json({ error: "istg" });
        }

        return res.status(200).json(user.discordId);
    } catch (error) {
        return res.status(500).json({ error: "server error" });
    }
});

app.get("/launcher/getUsername", sendData(async (req, res) => {
    const { discordId } = req.query;
// i hate my life
    if (!discordId) {
        return res.status(400).send("id thing is required");
    }

    const user = await User.findOne({ discordId });

    if (!user) {
        return res.status(404).json({ error: "User not found" });
    }

    return user.username;
}));

app.get("/fetch/news1", sendData(async (req, res) => {
    const newsContent = {
        header: "Flipped",
        date: "2024-05-27",
        desc: "Welcome to Flipped! We host 19.10 Europe and North America, we are the #1 leading OGFN community, come in and join!."
    };

    return newsContent;
}));

app.get("/fetch/news2", sendData(async (req, res) => {
    const newsContent = {
        header: "Version 1.0.0",
        date: "2024-05-27",
        desc: "Flipped has officially released. Swing around the map using Spider-Man's Web Shooters!"
    };

    return newsContent;
}));

app.get("/fetch/news3", sendData(async (req, res) => {
    const newsContent = {
        header: "XP and Quests",
        date: "2024-05-27",
        desc: "Gear up for quests and challenges in Flipped. Exciting adventures await as you explore the map and discover new locations. Don't miss out on the action!"
    };
// this is taking 15 years
    return newsContent;
}));

app.get("/launcher/getAvatar", async (req, res) => {
    try {
        const { discordId } = req.query;

        if (!discordId) {
            return res.status(400).send("id is required");
        }

        const user = await User.findOne({ discordId });

        if (!user) {
            return res.status(404).json({ error: "user not found" });
        }
        const avatarUrl = user.avatar;

        log.launcher(`Getting avatar ${avatarUrl} for ${discordId}`);

        return res.json(avatarUrl);
    } catch (error) {
        console.error(`Exception: ${error.message}`);
        return res.status(500).send("Internal Server Error");
    }
});


app.get("/selectedSkin", sendData(async (req, res) => {
    const { discordId } = req.query;
    // this took about an hour to figure out :sob:
    if (!discordId) {
        return res.status(400).send("id is required");
    }

    const user = await User.findOne({ discordId });

    if (!user) {
        return res.status(404).json({ error: "user not found" });
    }

    const accountId = user.accountId;  

    if (!accountId) {
        return res.status(400).json({ error: "id not found for user" });
    }

    const profile = await Profiles.findOne({ accountId });

    if (!profile) {
        return res.status(404).json({ error: "profile not found" });
    }

    const playercidthingy = profile.profiles.athena.stats.attributes.favorite_character;

    let cidthingy;
    if (!playercidthingy) {
        cidthingy = "CID_001_Athena_Commando_F_Default";
    } else {
        cidthingy = playercidthingy.replace('AthenaCharacter:', '');
    }

    try {
        const response = await axios.get(`https://fortnite-api.com/v2/cosmetics/br/${cidthingy}`);
        const iconUrl = response.data.data.images.icon;
        log.launcher(`Icon url thingy: ${iconUrl}`)
        if (!iconUrl) {
            console.error("icon not found");
            return res.status(404).json({ error: "icon aint found" });
        }

        return res.json(iconUrl);
    } catch (error) {
        log.launcher(`failed: ${error.message}`);
        return res.status(500).json({ error: "Fortnite" });
    }
}));
// i dont think vbucks works yet, walter or wiks will have to fix this prob
app.get("/profile/vbucks", sendData(async (req, res) => {
    const { discordId } = req.query;

    if (!discordId) {
        return res.status(400).send("id is required");
    }

    const user = await User.findOne({ discordId });

    if (!user) {
        return res.status(404).json({ error: "user not found" });
    }

    const profile = await Profiles.findOne({ accountId: user.accountId });

    if (!profile) {
        return res.status(404).json({ error: "profile thing not found" });
    }

    const items = profile && profile.profiles && profile.profiles.common_core && profile.profiles.common_core.athena && profile.profiles.common_core.athena.items;
    const vbucksBalance = items && items["Currency:MtxPurchased"] && items["Currency:MtxPurchased"].quantity || 0;
    
    console.log(`erm: ${vbucksBalance}`);

    res.json(vbucksBalance);
}));

app.get("/fetch/exchange_code", async (req, res) => {
    const { discordId, hwid } = req.query;

    console.log(`trying to auth this discordid ${discordId}`);

    try {

        const user = await User.findOne({ discordId }).lean();

        if (!user) {
            console.log(`user aint found`);
            return res.status(404).send;
        }

        if (user.hwid !== hwid) {
            console.log(`hwid wrongy nooo`);
            return res.status(401).send;
        }

        console.log(`auth fr worked for ${discordId}`);

        let exchange_code = functions.MakeID().replace(/-/ig, "");

        global.exchangeCodes.push({
            accountId: user.accountId,
            exchange_code: exchange_code,
            creatingClientId: ""
        });

        setTimeout(() => {
            let exchangeCodeIndex = global.exchangeCodes.findIndex(i => i.exchange_code == exchange_code);
            if (exchangeCodeIndex != -1) global.exchangeCodes.splice(exchangeCodeIndex, 1);
        }, 300000);

        res.status(200).send(exchange_code);
    } catch (error) {
        console.error(`issues but i cba ${discordId}`, error);
        res.status(500).send("thingy");
    }
});

export default app;```
