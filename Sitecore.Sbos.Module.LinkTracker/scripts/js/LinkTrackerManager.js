var triggerCount = 0; GoalCheck = "", PageEventCheck = "", CampaignCheck = "", popupCheck = 0;

function triggerGoal(goalId, shouldTriggerGoal, goalData) {
    $.ajax({
        url: "/Events/Handler/TrackedLinkHandler.ashx",
        type: "GET",
        data: { gid: goalId, triggerGoal: shouldTriggerGoal, goalData },
        context: this,
        success: function (data) {
            /*GoalCheck = shouldTriggerGoal;
            triggerCount++;
            TriggeredEventOnClick();
            alert("Goal is triggered", data);
            */
        },
        error: function (data) {
            alert("Goal is not been triggered", data);
        }
    });
}

function triggerPageEvent(pageEventId, shouldTriggerPageEvent, pageEventData) {
    $.ajax({
        url: "/Events/Handler/TrackedLinkHandler.ashx",
        type: "GET",
        data: { peid: pageEventId, triggerPageEvent: shouldTriggerPageEvent, pageEventData },
        context: this,
        success: function (data) {           
            /*PageEventCheck = shouldTriggerPageEvent;
            triggerCount++;
            TriggeredEventOnClick();
            alert("PageEvent is triggered", data);
            */

        },
        error: function (data) {
            alert("PageEvent is not been triggered", data);
        }
    });
}

function triggerCampaign(campaignId, shouldTriggerCampaign, campaignData) {
    $.ajax({
        url: "/Events/Handler/TrackedLinkHandler.ashx",
        type: "GET",
        data: { cid: campaignId, triggerCampaign: shouldTriggerCampaign, campaignData },
        context: this,
        success: function (data) {
            /*CampaignCheck = shouldTriggerCampaign;
            triggerCount++;
            TriggeredEventOnClick();
            alert("Campaign is triggered", data);
            */
            if(shouldTriggerCampaign == "true")
            {
                alert("Campaign is triggered", data);
            }
        },
        error: function (data) {
            alert("Campaign is not been triggered", data);
        }
    });
}
/*
function TriggeredEventOnClick() {
    var msg = "";
    
    if ((GoalCheck != "" && PageEventCheck != "" && CampaignCheck != "") && popupCheck == 1) {
        popupCheck = 0;
    }

    if (triggerCount == 2 && popupCheck == 0) {
        if (GoalCheck == "true") {
            msg += "Goal ";
        }

        if (PageEventCheck == "true") {
            msg += "PageEvent ";
        }

        if (CampaignCheck == "true") {
            msg += "Campaign ";
        }

        if (PageEventCheck == "true" && GoalCheck == "true" && CampaignCheck == "true") {
            msg += "and ";
        }

        if (PageEventCheck == "true" && GoalCheck == "true" && CampaignCheck == "true") {
            msg += "are ";
        } else {
            msg += "is ";
        }

        msg += "Triggered";

        if (PageEventCheck == "false" && GoalCheck == "false" && CampaignCheck == "false")
            popupCheck = 1;

        if(popupCheck == 0)
            alert(msg);

        popupCheck = 1;

        GoalCheck = "";
        PageEventCheck = "";
        CampaignCheck = "";
        triggerCount = 0;
    }
}
*/