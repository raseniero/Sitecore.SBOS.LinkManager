function activateSelectedGoal(goalId, isTriggerGoal)
{   
    $.ajax({
        url: "/Goals/Handler/GoalLinkTrackerHandler.ashx",
        type: "GET",
        data: { gid: goalId, triggerGoal: isTriggerGoal },
        context: this,
        success: function (data) {
            if (isTriggerGoal == "true") {
                alert("Goal has been triggered", data);
            }
            else {
                alert("Goal is not been triggered", data);
            }
        },
        error: function (data) {
            alert("Goal is not been triggered", data);
        }

    });
}
